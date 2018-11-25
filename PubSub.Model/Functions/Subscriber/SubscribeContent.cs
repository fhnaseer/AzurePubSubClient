using System.Collections.Generic;
using PubSub.Model.Responses;

namespace PubSub.Model.Functions.Subscriber
{
    public class SubscribeContent : PostServerlessFunctionBase
    {
        public SubscribeContent(string baseAddress)
            : base(baseAddress)
        {
        }

        public override string Name => "Subscribe Content";

        protected override string FunctionRelativeAddress => "subscribecontent";

        private MessageBase _sampleMessageInput;
        public override MessageBase SampleMessageInput => _sampleMessageInput ?? (_sampleMessageInput = new ContentsInput
        {
            SubscriberId = "subscriber043d4fa0518411e89a9d1bd8d0d9e684",
            Topics = new List<KeyValueContent>
            {
                new KeyValueContent { Key = "computer", Value = "intel", Condition = "="},
                new KeyValueContent { Key = "money", Value = "500", Condition = ">="}
            }
        });
    }
}
