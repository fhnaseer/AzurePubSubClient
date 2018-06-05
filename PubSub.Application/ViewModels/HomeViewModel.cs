using System.Collections.ObjectModel;
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
                Subscribers.Add(new SubscriberViewModel(_cloudProvider));
        }

        internal HomeViewModel() { }

        private ObservableCollection<SubscriberViewModel> _subscribers;
        public ObservableCollection<SubscriberViewModel> Subscribers => _subscribers ?? (_subscribers = new ObservableCollection<SubscriberViewModel>());

        private SubscriberViewModel _selectedSubscriber;
        public SubscriberViewModel SelectedSubscriber
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

        //private SubscriberViewModel _subscriberViewModel;
        //public SubscriberViewModel SubscriberViewModel => _subscriberViewModel ?? (_subscriberViewModel = new SubscriberViewModel(_cloudProvider));

        //private PublisherViewModel _publisherViewModel;
        //public PublisherViewModel PublisherViewModel => _publisherViewModel ?? (_publisherViewModel = new PublisherViewModel(_cloudProvider));
    }
}
