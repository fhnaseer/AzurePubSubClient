using System.Collections.Generic;
using PubSub.Model.Responses;

namespace PubSub.Model.Functions.Subscriber
{
    public class SubscribeTopic : PostServerlessFunctionBase
    {
        public SubscribeTopic(string baseAddress) : base(baseAddress)
        {
        }

        public override string Name => "Subscribe Topics,";

        protected override string FunctionRelativeAddress => "subscribetopic";

        private MessageBase _sampleMessageInput;
        public override MessageBase SampleMessageInput => _sampleMessageInput ?? (_sampleMessageInput = new TopicsInput
        {
            SubscriberId = "subscriber043d4fa0518411e89a9d1bd8d0d9e684",
            Topics = new List<string> { "computer", "science" }
        });
    }
}
