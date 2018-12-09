using Newtonsoft.Json;

namespace PubSub.Model.Responses
{
    public class FunctionInput : MessageBase
    {
        [JsonProperty("subscriptionType")]
        public string SubscriptionType { get; set; }

        [JsonProperty("functionType")]
        public string FunctionType { get; set; }

        [JsonProperty("matchingFunction")]
        public string MatchingFunction { get; set; }
    }
}
