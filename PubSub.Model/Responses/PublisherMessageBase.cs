using Newtonsoft.Json;

namespace PubSub.Model.Responses
{
    public class PublisherMessageBase : MessageBase
    {
        [JsonProperty("message")]
        public string Message { get; set; } = "Some Message;";
    }
}
