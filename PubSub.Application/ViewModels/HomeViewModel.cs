using PubSub.Model;

namespace PubSub.Application.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        private string _baseAddress = "http://localhost:7071";
        public string BaseAddress
        {
            get => _baseAddress;
            set
            {
                _baseAddress = value;
                SubscriberViewModel.AzureContext = new AzureContext(_baseAddress);
                PublisherViewModel.AzureContext = new AzureContext(_baseAddress);
                OnPropertyChanged(nameof(BaseAddress));
            }
        }

        private SubscriberViewModel _subscriberViewModel;
        public SubscriberViewModel SubscriberViewModel => _subscriberViewModel ?? (_subscriberViewModel = new SubscriberViewModel());

        private PublisherViewModel _publisherViewModel;
        public PublisherViewModel PublisherViewModel => _publisherViewModel ?? (_publisherViewModel = new PublisherViewModel());
    }
}
