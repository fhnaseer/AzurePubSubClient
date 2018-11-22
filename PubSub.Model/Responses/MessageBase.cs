using Newtonsoft.Json;
using System;

namespace PubSub.Model.Responses
{
    public class MessageBase
    {
        [JsonProperty("subscriberId")]
        public string SubscriberId { get; set; }

        [JsonProperty("message")] public string Message { get; set; } = "Some Message;";

        [JsonProperty("fromPublisher")] public string FromPublisher { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
