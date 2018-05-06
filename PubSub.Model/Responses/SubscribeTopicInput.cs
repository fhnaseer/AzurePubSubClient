using System.Collections.Generic;
using Newtonsoft.Json;

namespace PubSub.Model.Responses
{
    public class SubscribeTopicInput : MessageBase
    {
        [JsonProperty("subscriberId")]
        public string SubscriberId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        [JsonProperty("topics")]
        public List<string> Topics { get; set; }
    }
}
