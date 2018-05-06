using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using PubSub.Model;

namespace PubSub.Application.ViewModels
{
    public class SubscriberViewModel : ViewModelBase
    {
        private ICommand _registerSubscriberCommand;
        public ICommand RegisterSubscriberCommand => _registerSubscriberCommand ?? (_registerSubscriberCommand = new RelayCommand(RegisterSubscriber));

        private void RegisterSubscriber()
        {
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
