using Newtonsoft.Json;

namespace PubSub.Model.Responses
{
    public class MessageBase
    {
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
