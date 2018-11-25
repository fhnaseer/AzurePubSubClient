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
        public override MessageBase SampleMessageInput => _sampleMessageInput ?? (_sampleMessageInput = new FunctionInput
        {
            SubscriberId = "subscriber043d4fa0518411e89a9d1bd8d0d9e684",
            SubscriptionType = "Language",
            FunctionType = "url",
            MatchingFunction = "api/DetectLanguages"
        });
    }
}
