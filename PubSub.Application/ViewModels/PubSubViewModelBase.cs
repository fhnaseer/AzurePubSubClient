using System.Collections.ObjectModel;
using System.Windows.Input;
using PubSub.Model;
using PubSub.Model.Functions;

namespace PubSub.Application.ViewModels
{
    public abstract class PubSubViewModelBase : ViewModelBase
    {
        protected ConfigurationFile ConfigurationFile { get; }

        protected PubSubViewModelBase(ConfigurationFile configurationFile)
        {
            ConfigurationFile = configurationFile;
            CloudContext = new CloudContext(ConfigurationFile.BaseUrl);
        }

        private readonly string _baseAddress = "http://localhost:7071";
        public string BaseAddress => ConfigurationFile == null ? _baseAddress : ConfigurationFile.BaseUrl;

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
                _sampleMessageInput = _selectedFunction.SampleMessageInput.ToString();
                OnPropertyChanged(nameof(SampleMessageInput));
            }
        }

        private string _sampleMessageInput;
        public string SampleMessageInput
        {
            get => _sampleMessageInput;
            set
            {
                _sampleMessageInput = value;
                OnPropertyChanged(nameof(SampleMessageInput));
            }
        }

        private ICommand _executeFunctionCommand;
        public ICommand ExecuteFunctionCommand => _executeFunctionCommand ?? (_executeFunctionCommand = new RelayCommand(ExecuteFunction, CanExecuteFunction));

        internal bool CanExecuteFunction() { return SelectedFunction != null; }

        public abstract void ExecuteFunction();
    }
}
