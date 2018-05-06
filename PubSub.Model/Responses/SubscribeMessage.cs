using Newtonsoft.Json;

namespace PubSub.Model.Responses
{
    public class SubscribeMessage
    {
        [JsonProperty("id")]
        public string SubscriberId { get; set; }
    }
}
