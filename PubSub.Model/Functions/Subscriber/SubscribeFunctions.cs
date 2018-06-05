using PubSub.Model.Responses;

namespace PubSub.Model.Functions.Subscriber
{
    public class SubscribeFunctions : PostServerlessFunctionBase
    {
        public SubscribeFunctions(string baseAddress)
            : base(baseAddress)
        {
        }

        public override string Name => "Subscribe Function,";

        protected override string FunctionRelativeAddress => "SubscribeFunction";

        public override MessageBase SampleMessageInput => new FunctionInput
        {
            SubscriberId = "subscriber043d4fa0518411e89a9d1bd8d0d9e684",
            SubscriptionType = "Text",
            MatchingInputs = "index",
            MatchingFunction = "let populations = {'New Zealand': 4693000, 'Germany': 8267000}; return populations[index];"
        };
    }
}
