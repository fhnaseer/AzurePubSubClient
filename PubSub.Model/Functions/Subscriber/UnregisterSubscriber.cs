using System.Threading.Tasks;
using PubSub.Model.Responses;

namespace PubSub.Model.Functions.Subscriber
{
    public class UnregisterSubscriber : ServerlessFunctionBase
    {
        public UnregisterSubscriber(string baseAddress)
            : base(baseAddress)
        {
        }

        public override string Name => "Unregister Subscriber,";

        protected override string FunctionRelativeAddress => "UnregisterSubscriber";

        public override async Task<string> ExecuteFunction(object parameters)
        {
            return await HttpRestClient.Post(FunctionAddress, parameters);
        }

        public override MessageBase SampleMessageInput => new UnregisterSubscriberInput
        {
            SubscriberId = "subscriber043d4fa0518411e89a9d1bd8d0d9e684"
        };
    }
}
