using System.Collections.Generic;
using Newtonsoft.Json;

namespace PubSub.Model.Responses
{
    public class SubscribeTopicMessage : MessageBase
    {
        [JsonProperty("subscriberId")]
        public string SubscriberId { get; set; }

        [JsonProperty("topics")]
        public List<string> Topics { get; set; }
    }
}
