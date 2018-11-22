using System;
using Amazon;
using Amazon.Runtime;
using Amazon.SQS;
using Amazon.SQS.Model;
using Newtonsoft.Json.Linq;

namespace PubSub.Application.BrokerEntities
{
    public class AwsSubscriber : Subscriber
    {
        private AmazonSQSClient _amazonClient;

        public override void SetupMessageQueue()
        {
            if (_amazonClient != null) return;
            var credentials = new BasicAWSCredentials("AKIAIKBFSSFHFA3EMXAA", "gZu7M14AufPLtGqP/NZjfySOHk6r5mqYzwyBfW9f");
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
                QueueUrl = SubscribeResponse.QueueUrl,
                WaitTimeSeconds = 20,
            };

            while (true)
            {
                var response = await _amazonClient.ReceiveMessageAsync(request);
                foreach (var message in response.Messages)
                {
                    var currentTime = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff");
                    var json = JObject.Parse(message.Body);
                    var fromPublisher = Convert.ToDateTime(json["fromPublisher"]);
                    var functionInvoked = Convert.ToDateTime(json["functionInvoked"]);
                    var databaseAccessed = Convert.ToDateTime(json["databaseAccessed"]);
                    var current = Convert.ToDateTime(currentTime);
                    var serverlessInvoked = (functionInvoked - fromPublisher).TotalMilliseconds;
                    var database = (databaseAccessed - functionInvoked).TotalMilliseconds;
                    var received = (current - databaseAccessed).TotalMilliseconds;
                    var total = (current - fromPublisher).TotalMilliseconds;
                    AppendText($"Total Time: {total} Function Invoked: {serverlessInvoked}, Database: {database}, Received: {received}, Message: {total}");
                    _amazonClient.DeleteMessageAsync(SubscribeResponse.QueueUrl, message.ReceiptHandle);
                }
            }
        }
    }
}
