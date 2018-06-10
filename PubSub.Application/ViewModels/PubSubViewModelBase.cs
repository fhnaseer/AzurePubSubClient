using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using PubSub.Model;
using PubSub.Model.Functions;

namespace PubSub.Application.ViewModels
{
    public abstract class PubSubViewModelBase : ViewModelBase
    {
        protected CloudProviderMetadata CloudProvider { get; }

        protected PubSubViewModelBase(CloudProviderMetadata cloudProvider)
        {
            CloudProvider = cloudProvider;
            CloudContext = new CloudContext(CloudProvider.BaseAddress);
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

        public virtual async void ExecuteFunction()
        {
            var response = await SelectedFunction.ExecuteFunction(SelectedFunction.SampleMessageInput);
            AppendText(response);
        }

        private readonly StringBuilder _messageResponses = new StringBuilder();

        public string MessageResponses => _messageResponses.ToString();

        public void AppendText(string nextLine)
        {
            _messageResponses.AppendLine(nextLine);
            OnPropertyChanged(nameof(MessageResponses));
        }
    }
}
