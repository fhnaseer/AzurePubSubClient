using Newtonsoft.Json;

namespace PubSub.Model.Responses
{
    public class SubscribeMessage : MessageBase
    {
        [JsonProperty("id")]
        public string SubscriberId { get; set; }
    }
}
