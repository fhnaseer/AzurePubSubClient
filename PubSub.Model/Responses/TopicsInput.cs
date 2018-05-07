using System.Collections.Generic;
using Newtonsoft.Json;

namespace PubSub.Model.Responses
{
    public class TopicsInput : MessageBase
    {
        [JsonProperty("subscriberId")]
        public string SubscriberId { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        [JsonProperty("topics")]
        public List<string> Topics { get; set; }
    }
}
