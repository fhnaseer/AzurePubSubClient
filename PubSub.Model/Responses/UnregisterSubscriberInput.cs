using Newtonsoft.Json;

namespace PubSub.Model.Responses
{
    public class UnregisterSubscriberInput : SubscriberMessageBase
    {
        [JsonProperty("subscriberId")]
        public string SubscriberId { get; set; }
    }
}
