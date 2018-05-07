using Newtonsoft.Json;

namespace PubSub.Model.Responses
{
    public class SubscribeFunctionInput : MessageBase
    {
        [JsonProperty("subscriberId")]
        public string SubscriberId { get; set; }

        [JsonProperty("subscriptionType")]
        public string SubscriptionType { get; set; }

        [JsonProperty("matchingInputs")]
        public string MatchingInputs { get; set; }

        [JsonProperty("matchingFunction")]
        public string MatchingFunction { get; set; }
    }
}
