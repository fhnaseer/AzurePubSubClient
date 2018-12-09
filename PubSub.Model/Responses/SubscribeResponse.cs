using Newtonsoft.Json;

namespace PubSub.Model.Responses
{
    public class SubscribeResponse : SubscriberMessageBase
    {
        [JsonProperty("subscriberId")]
        public string SubscriberId { get; set; }

        [JsonProperty("connectionString")]
        public string ConnectionString { get; set; }

        [JsonProperty("queueName")]
        public string QueueName { get; set; }

        [JsonProperty("QueueUrl")]
        public string QueueUrl { get; set; }
    }
}
