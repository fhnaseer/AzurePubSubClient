using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PubSub.Model.Responses;

namespace PubSub.Model
{
    public class SubscribeTopicFunction : ServerlessFunctionBase
    {
        public SubscribeTopicFunction(string baseAddress) : base(baseAddress)
        {
        }

        public override string Name => "Subscribe Topics";

        protected override string FunctionRelativeAddress => "SubscribeTopic";

        public override Task<string> ExecuteFunction(object parameters)
        {
            throw new NotImplementedException();
        }

        private MessageBase _sampleMessageInput;
        public override MessageBase SampleMessageInput => _sampleMessageInput ?? (_sampleMessageInput = new SubscribeTopicMessage
        {
            SubscriberId = "subscriber043d4fa0518411e89a9d1bd8d0d9e684",
            Topics = new List<string> { "computer", "science" }
        });
    }
}
