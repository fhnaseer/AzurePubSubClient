using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using PubSub.Model;
using PubSub.Model.Responses;

namespace PubSub.Application.ViewModels
{
    public class AzureSubscriberViewModel : SubscriberViewModelBase
    {
        public AzureSubscriberViewModel(CloudProviderMetadata cloudProvider) : base(cloudProvider)
        {
            Functions.Clear();
            var functions = CloudContext.GetSubscriberFunctions();
            foreach (var function in functions)
                Functions.Add(function);
        }

        internal AzureSubscriberViewModel() : base(null) { }

        protected override void OnRegisterSubscriber(string response)
        {
            var subscribeResponse = JsonConvert.DeserializeObject<SubscribeResponse>(response);
            SubscriberId = subscribeResponse.QueueName;
            ConnectionString = subscribeResponse.ConnectionString;
            SetupMessageQueue(subscribeResponse);
        }

        public string ConnectionString { get; set; }

        private IQueueClient _messageQueue;

        private void SetupMessageQueue(SubscribeResponse subscribeResponse)
        {
            if (_messageQueue != null) return;
            _messageQueue = new QueueClient(subscribeResponse.ConnectionString, subscribeResponse.QueueName);
            var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                MaxConcurrentCalls = 1,
                AutoComplete = false
            };
            _messageQueue.RegisterMessageHandler(ProcessMessagesAsync, messageHandlerOptions);
        }

        private async Task ProcessMessagesAsync(Message message, CancellationToken token)
        {
            AppendText($"Received message: SequenceNumber:{message.SystemProperties.SequenceNumber} Body:{Encoding.UTF8.GetString(message.Body)}");
            await _messageQueue.CompleteAsync(message.SystemProperties.LockToken);
        }

        private static Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            //AppendText($"Message handler encountered an exception {exceptionReceivedEventArgs.Exception}.");
            //var context = exceptionReceivedEventArgs.ExceptionReceivedContext;
            //AppendText("Exception context for troubleshooting:");
            //AppendText($"- Endpoint: {context.Endpoint}");
            //AppendText($"- Entity Path: {context.EntityPath}");
            //AppendText($"- Executing Action: {context.Action}");
            return Task.CompletedTask;
        }
    }
}
