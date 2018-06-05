using Newtonsoft.Json;

namespace PubSub.Model.Responses
{
    public class MessageBase
    {
        [JsonProperty("subscriberId")]
        public string SubscriberId { get; set; }

        [JsonProperty("message")] public string Message { get; set; } = "Some Message;";

        public override string ToString()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }
    }
}
