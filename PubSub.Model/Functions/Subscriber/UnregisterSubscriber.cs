using PubSub.Model.Responses;

namespace PubSub.Model.Functions.Subscriber
{
    public class UnregisterSubscriber : PostServerlessFunctionBase
    {
        public UnregisterSubscriber(string baseAddress)
            : base(baseAddress)
        {
        }

        public override string Name => "Unregister Subscriber,";

        protected override string FunctionRelativeAddress => "UnregisterSubscriber";

        public override MessageBase SampleMessageInput => new UnregisterSubscriberInput
        {
            SubscriberId = "subscriber043d4fa0518411e89a9d1bd8d0d9e684"
        };
    }
}
