namespace PubSubClient.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        private SubscriberViewModel _subscriberViewModel;
        public SubscriberViewModel SubscriberViewModel => _subscriberViewModel ?? (_subscriberViewModel = new SubscriberViewModel());

        private PublisherViewModel _publisherViewModel;
        public PublisherViewModel PublisherViewModel => _publisherViewModel ?? (_publisherViewModel = new PublisherViewModel());
    }
}
