namespace PubSub.Model.Responses
{
    public class MessageBase
    {
        public override string ToString()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }
    }
}
