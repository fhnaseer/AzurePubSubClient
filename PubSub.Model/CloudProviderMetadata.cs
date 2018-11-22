using Newtonsoft.Json;

namespace PubSub.Model
{
    public enum ProviderType
    {
        Azure,
        Aws
    }

    public enum ApplicationMode
    {
        Subscriber,
        Publisher,
        Mixed,
    }

    public class ConfigurationFile
    {
        private string _baseUrl;
        [JsonProperty("baseUrl")]
        public string BaseUrl
        {
            get => _baseUrl?.Trim();
            set => _baseUrl = value;
        }

        [JsonProperty("providerType")]
        public ProviderType ProviderType { get; set; }

        [JsonProperty("applicationMode")]
        public ApplicationMode ApplicationMode { get; set; }

        [JsonProperty("nodesCount")]
        public int NodesCount { get; set; }
    }
}
