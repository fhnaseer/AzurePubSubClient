using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
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
            SubscriberId = JsonConvert.DeserializeObject<SubscribeMessage>(response).SubscriberId;
            AppendText(response);
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

        private ObservableCollection<IServerlessFunction> _functions;
        public ObservableCollection<IServerlessFunction> Functions => _functions ?? (_functions = new ObservableCollection<IServerlessFunction>());

        private IServerlessFunction _selectedFunction;
        public IServerlessFunction SelectedFunction
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

        private void ExecuteFunction()
        {
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
