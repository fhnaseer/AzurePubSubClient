using Amazon;
using Amazon.Runtime;
using Amazon.SQS;
using Amazon.SQS.Model;
using PubSub.Model;

namespace PubSub.Application.ViewModels
{
    public class AwsSubscriberViewModel : SubscriberViewModelBase
    {
        public AwsSubscriberViewModel(CloudProviderMetadata cloudProvider) : base(cloudProvider)
        {
            Functions.Clear();
            var functions = CloudContext.GetSubscriberFunctions();
            foreach (var function in functions)
                Functions.Add(function);
        }

        internal AwsSubscriberViewModel() : base(null) { }

        private AmazonSQSClient _amazonClient;

        protected override void SetupMessageQueue()
        {
            if (_amazonClient != null) return;
            var credentials = new AnonymousAWSCredentials();
            _amazonClient = new AmazonSQSClient(credentials, RegionEndpoint.EUCentral1);
            FetchMessages();
        }

        private async void FetchMessages()
        {
            var request = new ReceiveMessageRequest
            {
                AttributeNames = { "SentTimestamp" },
                MaxNumberOfMessages = 1,
                MessageAttributeNames = { "All" },
                QueueUrl = Subscriber.QueueUrl,
                WaitTimeSeconds = 20,
            };

            while (true)
            {
                var response = await _amazonClient.ReceiveMessageAsync(request);
                foreach (var message in response.Messages)
                    AppendText(message.Body);
            }
        }
    }
}
