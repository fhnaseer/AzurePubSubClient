using PubSub.Model;

namespace PubSub.Application.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        private string _baseUrl;
        public string BaseUrl
        {
            get => _baseUrl;
            set
            {
                _baseUrl = value;
                SubscriberViewModel.AzureContext = new AzureContext(_baseUrl);
                PublisherViewModel.AzureContext = new AzureContext(_baseUrl);
                OnPropertyChanged(nameof(BaseUrl));
            }
        }

        private SubscriberViewModel _subscriberViewModel;
        public SubscriberViewModel SubscriberViewModel => _subscriberViewModel ?? (_subscriberViewModel = new SubscriberViewModel());

        private PublisherViewModel _publisherViewModel;
        public PublisherViewModel PublisherViewModel => _publisherViewModel ?? (_publisherViewModel = new PublisherViewModel());
    }
}
