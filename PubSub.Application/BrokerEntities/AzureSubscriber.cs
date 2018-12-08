using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json.Linq;
using PubSub.Model;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PubSub.Application.BrokerEntities
{
    public class AzureSubscriber : Subscriber
    {
        public AzureSubscriber(CloudContext cloudContext) : base(cloudContext)
        {
        }

        private IQueueClient _messageQueue;

        public override void SetupMessageQueue()
        {
            if (_messageQueue != null) return;
            _messageQueue = new QueueClient(SubscribeResponse.ConnectionString, SubscribeResponse.QueueName);
            var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                MaxConcurrentCalls = 1,
                AutoComplete = false
            };
            _messageQueue.RegisterMessageHandler(ProcessMessagesAsync, messageHandlerOptions);
        }

        private async Task ProcessMessagesAsync(Message message, CancellationToken token)
        {
            var json = JObject.Parse(Encoding.UTF8.GetString(message.Body));
            AppendText($"Type: {json["type"]}, Message: {json["message"]}");
            //AppendText($"Received message: SequenceNumber:{message.SystemProperties.SequenceNumber} Body:{Encoding.UTF8.GetString(message.Body)}");
            await _messageQueue.CompleteAsync(message.SystemProperties.LockToken);
        }

        private static Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            return Task.CompletedTask;
        }
    }
}
