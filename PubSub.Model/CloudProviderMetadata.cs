namespace PubSub.Model
{
    public enum CloudProvider
    {
        Azure,
        Aws
    }

    public class CloudProviderMetadata
    {
        public CloudProvider CloudProvider { get; set; }

        public int PublishersCount { get; set; }

        public int SubscribersCount { get; set; }

        private string _baseAddress;

        public string BaseAddress
        {
            get => _baseAddress?.Trim();
            set => _baseAddress = value;
        }
    }
}
