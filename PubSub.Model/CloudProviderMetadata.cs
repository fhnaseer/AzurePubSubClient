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

        public string BaseAddress { get; set; }
    }
}
