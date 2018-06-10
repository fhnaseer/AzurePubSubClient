using System.Collections.ObjectModel;
using System.Windows.Input;
using PubSub.Model;

namespace PubSub.Application.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        private readonly CloudProviderMetadata _cloudProvider;

        public HomeViewModel(CloudProviderMetadata cloudProvider)
        {
            _cloudProvider = cloudProvider;
            for (var i = 0; i < _cloudProvider.PublishersCount; i++)
                Publishers.Add(new PublisherViewModel(_cloudProvider));
            for (var i = 0; i < _cloudProvider.SubscribersCount; i++)
            {
                if (_cloudProvider.CloudProvider == CloudProvider.Azure)
                    Subscribers.Add(new AzureSubscriberViewModel(_cloudProvider));
                else
                    Subscribers.Add(new AwsSubscriberViewModel(_cloudProvider));
            }
        }

        internal HomeViewModel() { }

        private ObservableCollection<SubscriberViewModelBase> _subscribers;
        public ObservableCollection<SubscriberViewModelBase> Subscribers => _subscribers ?? (_subscribers = new ObservableCollection<SubscriberViewModelBase>());

        private SubscriberViewModelBase _selectedSubscriber;
        public SubscriberViewModelBase SelectedSubscriber
        {
            get => _selectedSubscriber;
            set
            {
                _selectedSubscriber = value;
                OnPropertyChanged(nameof(SelectedSubscriber));
                OnPropertyChanged(nameof(SubscriberIndex));
            }
        }

        public int SubscriberIndex { get; set; }

        private ObservableCollection<PublisherViewModel> _publishers;
        public ObservableCollection<PublisherViewModel> Publishers => _publishers ?? (_publishers = new ObservableCollection<PublisherViewModel>());

        private PublisherViewModel _selectedPublisher;
        public PublisherViewModel SelectedPublisher
        {
            get => _selectedPublisher;
            set
            {
                _selectedPublisher = value;
                OnPropertyChanged(nameof(SelectedPublisher));
                OnPropertyChanged(nameof(PublisherIndex));
            }
        }

        public int PublisherIndex { get; set; }

        private readonly string _baseAddress = "http://localhost:7071";
        public string BaseAddress => _cloudProvider == null ? _baseAddress : _cloudProvider.BaseAddress;

        private ICommand _registerSubscribersCommand;
        public ICommand RegisterSubscribersCommand => _registerSubscribersCommand ?? (_registerSubscribersCommand = new RelayCommand(RegisterSubscribers));

        internal void RegisterSubscribers()
        {
            foreach (var subscriber in Subscribers)
                subscriber.RegisterSubscriber();
        }

        private ICommand _unregisterSubscribersCommand;
        public ICommand UnregisterSubscribersCommand => _unregisterSubscribersCommand ?? (_unregisterSubscribersCommand = new RelayCommand(UnregisterSubscribers));

        internal void UnregisterSubscribers()
        {
            foreach (var subscriber in Subscribers)
                subscriber.UnregisterSubscriber();
        }
    }
}
