using PubSub.Model.Responses;

namespace PubSub.Model.Functions.Subscriber
{
    public class SubscribeFunctions : PostServerlessFunctionBase
    {
        public SubscribeFunctions(string baseAddress)
            : base(baseAddress)
        {
        }

        public override string Name => "Subscribe Function";

        protected override string FunctionRelativeAddress => "subscribefunction";

        private MessageBase _sampleMessageInput;
        public override MessageBase SampleMessageInput => _sampleMessageInput ?? (_sampleMessageInput = new SubscribeFunctionInput
        {
            SubscriptionType = "English",
            FunctionType = "url",
            MatchingFunction = "http://pubssubfunctions.azurewebsites.net"
        });
    }
}
