using Newtonsoft.Json;

namespace PubSub.Model.Responses
{
    public class UnregisterSubscriberInput : MessageBase
    {
        [JsonProperty("id")]
        public string SubscriberId { get; set; }
    }
}
