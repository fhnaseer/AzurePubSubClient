using Newtonsoft.Json;

namespace PubSub.Model.Responses
{
    public class SubscribeFunctionInput : SubscriberMessageBase
    {
        [JsonProperty("subscriptionType")]
        public string SubscriptionType { get; set; }

        [JsonProperty("functionType")]
        public string FunctionType { get; set; }

        [JsonProperty("matchingFunction")]
        public string MatchingFunction { get; set; }
    }

    public class PublishFunctionInput : PublisherMessageBase
    {
    }
}
