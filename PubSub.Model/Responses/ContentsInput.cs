using System.Collections.Generic;
using Newtonsoft.Json;

namespace PubSub.Model.Responses
{
    public class ContentsInput : MessageBase
    {
        [JsonProperty("subscriberId")]
        public string SubscriberId { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        [JsonProperty("content")]
        public List<KeyValueContent> Topics { get; set; }
    }

    public class KeyValueContent
    {
        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("condition")]
        public string Condition { get; set; }
    }
}
