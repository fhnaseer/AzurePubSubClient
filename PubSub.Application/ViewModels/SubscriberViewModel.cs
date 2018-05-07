using System.Collections.ObjectModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using PubSub.Model;
using PubSub.Model.Responses;

namespace PubSub.Application.ViewModels
{
    public class SubscriberViewModel : ViewModelBase
    {
        private ICommand _registerSubscriberCommand;
        public ICommand RegisterSubscriberCommand => _registerSubscriberCommand ?? (_registerSubscriberCommand = new RelayCommand(RegisterSubscriber));

        private async void RegisterSubscriber()
        {
            var response = await AzureContext.RegisterSubscriberFunction.ExecuteFunction(null);
            SubscriberId = JsonConvert.DeserializeObject<RegisterSubscriberResponse>(response).SubscriberId;
            AppendText(response);
            Functions.Add(new SubscribeTopicFunction(AzureContext.BaseAddress));
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

        private ObservableCollection<ServerlessFunctionBase> _functions;
        public ObservableCollection<ServerlessFunctionBase> Functions => _functions ?? (_functions = new ObservableCollection<ServerlessFunctionBase>());

        private ServerlessFunctionBase _selectedFunction;
        public ServerlessFunctionBase SelectedFunction
        {
            get => _selectedFunction;
            set
            {
                _selectedFunction = value;
                OnPropertyChanged(nameof(SelectedFunction));
            }
        }

        private ICommand _executeFunctionCommand;
        public ICommand ExecuteFunctionCommand => _executeFunctionCommand ?? (_executeFunctionCommand = new RelayCommand(ExecuteFunction, CanExecuteFunction));

        internal bool CanExecuteFunction() { return SelectedFunction != null; }

        private async void ExecuteFunction()
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

        private readonly StringBuilder _subscriberText = new StringBuilder();
        public string SubscriberText => _subscriberText.ToString();

        private void AppendText(string nextLine)
        {
            _subscriberText.AppendLine(nextLine);
            OnPropertyChanged(nameof(SubscriberText));
        }
    }
}
