using PubSub.Model.Responses;

namespace PubSub.Model.Functions.Subscriber
{
    public class RegisterSubscriber : GetServerlessFunctionBase
    {
        public RegisterSubscriber(string baseAddress)
        : base(baseAddress)
        {
        }

        public override string Name => "Register Subscriber";

        protected override string FunctionRelativeAddress => "subscribe";

        public override MessageBase SampleMessageInput => new MessageBase();
    }
}
