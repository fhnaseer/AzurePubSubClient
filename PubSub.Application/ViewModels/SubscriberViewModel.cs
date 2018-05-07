using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using PubSub.Model.Responses;

namespace PubSub.Application.ViewModels
{
    public class SubscriberViewModel : PubSubViewModelBase
    {
        private ICommand _registerSubscriberCommand;
        public ICommand RegisterSubscriberCommand => _registerSubscriberCommand ?? (_registerSubscriberCommand = new RelayCommand(RegisterSubscriber));

        private async void RegisterSubscriber()
        {
            var response = await AzureContext.RegisterSubscriber.ExecuteFunction(null);
            SubscriberId = JsonConvert.DeserializeObject<RegisterSubscriberResponse>(response).SubscriberId;
            AppendText(response);
            Functions.Clear();
            var functions = AzureContext.GetSubscriberFunctions();
            foreach (var function in functions)
                Functions.Add(function);
        }

        private string _subscriberId;
        public string SubscriberId
        {
            get => _subscriberId;
            set
            {
                _subscriberId = value;
                OnPropertyChanged(nameof(SubscriberId));
            }
        }

        public override async void ExecuteFunction()
        {
            var response = await SelectedFunction.ExecuteFunction(SelectedFunction.SampleMessageInput);
            AppendText(response);
            SetupMessageQueue(JsonConvert.DeserializeObject<SubscribeResponse>(response));
        }

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

        private Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            AppendText($"Message handler encountered an exception {exceptionReceivedEventArgs.Exception}.");
            var context = exceptionReceivedEventArgs.ExceptionReceivedContext;
            AppendText("Exception context for troubleshooting:");
            AppendText($"- Endpoint: {context.Endpoint}");
            AppendText($"- Entity Path: {context.EntityPath}");
            AppendText($"- Executing Action: {context.Action}");
            return Task.CompletedTask;
        }
    }
}
