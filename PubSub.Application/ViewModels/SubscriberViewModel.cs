using System.Collections.ObjectModel;
using System.Windows.Input;
using PubSub.Application.BrokerEntities;
using PubSub.Model;

namespace PubSub.Application.ViewModels
{
    public class SubscriberViewModel : PubSubViewModelBase
    {
        public SubscriberViewModel(ConfigurationFile configurationFile) : base(configurationFile)
        {
            for (var i = 0; i < configurationFile.NodesCount; i++)
            {
                if (configurationFile.ProviderType == ProviderType.Azure)
                    Subscribers.Add(new AzureSubscriber());
                else
                    Subscribers.Add(new AwsSubscriber());
            }

            Functions.Clear();
            var functions = CloudContext.GetSubscriberFunctions();
            foreach (var function in functions)
                Functions.Add(function);
        }

        internal SubscriberViewModel() : base(null) { }

        private ObservableCollection<Subscriber> _subscribers;
        public ObservableCollection<Subscriber> Subscribers => _subscribers ?? (_subscribers = new ObservableCollection<Subscriber>());

        private Subscriber _selectedSubscriber;
        public Subscriber SelectedSubscriber
        {
            get => _selectedSubscriber;
            set
            {
                _selectedSubscriber = value;
                OnPropertyChanged(nameof(SelectedSubscriber));
            }
        }

        private bool _allSubscribers = true;
        public bool AllSubscribers
        {
            get => _allSubscribers;
            set
            {
                _allSubscribers = value;
                OnPropertyChanged(nameof(AllSubscribers));
            }
        }

        private bool _selectedSubscriberOnly;
        public bool SelectedSubscriberOnly
        {
            get => _selectedSubscriberOnly;
            set
            {
                _selectedSubscriberOnly = value;
                OnPropertyChanged(nameof(AllSubscribers));
            }
        }

        private ICommand _registerSubscribersCommand;
        public ICommand RegisterSubscribersCommand => _registerSubscribersCommand ?? (_registerSubscribersCommand = new RelayCommand(RegisterSubscribers));

        internal async void RegisterSubscribers()
        {
            if (AllSubscribers)
                foreach (var subscriber in Subscribers)
                    subscriber.RegisterSubscriber();
            else
                SelectedSubscriber?.RegisterSubscriber();
        }

        private ICommand _unregisterSubscribersCommand;
        public ICommand UnregisterSubscribersCommand => _unregisterSubscribersCommand ?? (_unregisterSubscribersCommand = new RelayCommand(UnregisterSubscribers));

        internal void UnregisterSubscribers()
        {
            if (AllSubscribers)
                foreach (var subscriber in Subscribers)
                    subscriber.UnregisterSubscriber();
            else
                SelectedSubscriber?.RegisterSubscriber();
        }

        public override async void ExecuteFunction()
        {
            if (AllSubscribers)
                foreach (var subscriber in Subscribers)
                {
                    SelectedFunction.SampleMessageInput.SubscriberId = subscriber.SubscriberId;
                    var response = await SelectedFunction.ExecuteFunction(SelectedFunction.SampleMessageInput);
                    subscriber.AppendText(response);
                }
            else
            {
                SelectedFunction.SampleMessageInput.SubscriberId = SelectedSubscriber.SubscriberId;
                var response = await SelectedFunction.ExecuteFunction(SelectedFunction.SampleMessageInput);
                SelectedSubscriber.AppendText(response);
            }
        }
    }
}
