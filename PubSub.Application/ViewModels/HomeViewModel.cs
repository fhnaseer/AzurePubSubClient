using PubSub.Model;

namespace PubSub.Application.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        private readonly CloudProviderMetadata _cloudProvider;

        public HomeViewModel(CloudProviderMetadata cloudProvider)
        {
            _cloudProvider = cloudProvider;
        }

        internal HomeViewModel() { }

        private readonly string _baseAddress = "http://localhost:7071";
        public string BaseAddress => _cloudProvider == null ? _baseAddress : _cloudProvider.BaseAddress;

        private SubscriberViewModel _subscriberViewModel;
        public SubscriberViewModel SubscriberViewModel => _subscriberViewModel ?? (_subscriberViewModel = new SubscriberViewModel(_cloudProvider));

        private PublisherViewModel _publisherViewModel;
        public PublisherViewModel PublisherViewModel => _publisherViewModel ?? (_publisherViewModel = new PublisherViewModel(_cloudProvider));
    }
}
