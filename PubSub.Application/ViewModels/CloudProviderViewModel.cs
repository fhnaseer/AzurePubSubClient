using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.Runtime;
using Amazon.SQS;
using Microsoft.ServiceBus;
using PubSub.Model;

namespace PubSub.Application.ViewModels
{
    public class CloudProviderViewModel : ViewModelBase
    {
        private ObservableCollection<ProviderType> _providers;
        public ObservableCollection<ProviderType> Providers => _providers ?? (_providers = new ObservableCollection<ProviderType>(new[] { ProviderType.Aws, ProviderType.Azure }));

        public ProviderType SelectedProvider
        {
            get => ConfigurationFile.ProviderType;
            set  {
                ConfigurationFile.ProviderType = value;
                if (ConfigurationFile.ProviderType == ProviderType.Aws)
                ConfigurationFile.BaseUrl = "https://0achmjvzf2.execute-api.eu-central-1.amazonaws.com/pubsub/csharp";
                else
                    ConfigurationFile.BaseUrl = "http://localhost:7071";
                OnPropertyChanged(nameof(ConfigurationFile));
                OnPropertyChanged(nameof(SelectedProvider));
            }
        }

        private ObservableCollection<ApplicationMode> _applicationModes;
        public ObservableCollection<ApplicationMode> ApplicationModes => _applicationModes ?? (_applicationModes = new ObservableCollection<ApplicationMode>(new[] { ApplicationMode.Subscriber, ApplicationMode.Publisher}));

        private ConfigurationFile _configurationFile;
        public ConfigurationFile ConfigurationFile
        {
            get => _configurationFile ?? (_configurationFile = new ConfigurationFile { ProviderType = ProviderType.Aws, ApplicationMode = ApplicationMode.Subscriber, BaseUrl = "https://0achmjvzf2.execute-api.eu-central-1.amazonaws.com/pubsub/csharp", NodesCount = 1});
            set
            {
                _configurationFile = value;
                OnPropertyChanged(nameof(ConfigurationFile));
            }
        }

        private ICommand _launchServerlessCommand;
        public ICommand LaunchServerlessCommand => _launchServerlessCommand ?? (_launchServerlessCommand = new RelayCommand(LaunchServerless, CanLaunchServerless));

        internal bool CanLaunchServerless() { return ConfigurationFile.BaseUrl != null && ConfigurationFile.NodesCount != 0; }

        internal void LaunchServerless()
        {
            var window = new MainWindow();
            if (ConfigurationFile.ApplicationMode == ApplicationMode.Subscriber)
                window.DataContext = new SubscriberViewModel(ConfigurationFile);
            else
                window.DataContext = new PublisherViewModel(ConfigurationFile);
            window.Show();
        }

        private ICommand _clearResourcesCommand;
        public ICommand ClearResourcesCommand => _clearResourcesCommand ?? (_clearResourcesCommand = new RelayCommand(ClearResources));

        internal async void ClearResources()
        {
            var credentials = new BasicAWSCredentials("AKIAIKBFSSFHFA3EMXAA", "gZu7M14AufPLtGqP/NZjfySOHk6r5mqYzwyBfW9f");
            var amazonClient = new AmazonSQSClient(credentials, RegionEndpoint.EUCentral1);
            var sqsResponse = amazonClient.ListQueues("subscriber");
            foreach (var url in sqsResponse.QueueUrls)
                amazonClient.DeleteQueueAsync(url);

            var dbClient = new AmazonDynamoDBClient(credentials, RegionEndpoint.EUCentral1);
            ClearDynamoDbTable(dbClient, "topics");
            ClearDynamoDbTable(dbClient, "content");
            ClearDynamoDbTable(dbClient, "functions");

            var manager = NamespaceManager.CreateFromConnectionString("Endpoint=sb://serverlessservicebus.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=lj8a0ng7f4KwQPizDEAcSAuxzt95su6RUb/wQ1Q9+k4=");
            var queues = await manager.GetQueuesAsync();
            foreach (var queueDescription in queues)
                manager.DeleteQueue(queueDescription.Path);

            MessageBox.Show("Resources Clear");
        }

        private static async void ClearDynamoDbTable(AmazonDynamoDBClient dbClient, string tableName)
        {
            var response = dbClient.DescribeTable(tableName);
            await dbClient.DeleteTableAsync(tableName);
            do
            {
                try
                {
                    dbClient.CreateTable(response.Table.TableName, response.Table.KeySchema, response.Table.AttributeDefinitions, new ProvisionedThroughput(100, 5));
                    return;
                }
                catch (Exception)
                {
                    Thread.Sleep(3000);
                }
            } while (true);
        }
    }
}
