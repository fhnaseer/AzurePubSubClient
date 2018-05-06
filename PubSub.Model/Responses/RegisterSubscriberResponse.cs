using Newtonsoft.Json;

namespace PubSub.Model.Responses
{
    public class RegisterSubscriberResponse : MessageBase
    {
        [JsonProperty("id")]
        public string SubscriberId { get; set; }
    }
}
