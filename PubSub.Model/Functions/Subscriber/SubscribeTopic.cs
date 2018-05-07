using System.Collections.Generic;
using System.Threading.Tasks;
using PubSub.Model.Responses;

namespace PubSub.Model.Functions.Subscriber
{
    public class SubscribeTopic : ServerlessFunctionBase
    {
        public SubscribeTopic(string baseAddress) : base(baseAddress)
        {
        }

        public override string Name => "Subscribe Topics,";

        protected override string FunctionRelativeAddress => "SubscribeTopic";

        public override async Task<string> ExecuteFunction(object parameters)
        {
            return await HttpRestClient.Post(FunctionAddress, parameters);
        }

        private MessageBase _sampleMessageInput;
        public override MessageBase SampleMessageInput => _sampleMessageInput ?? (_sampleMessageInput = new SubscribeTopicInput
        {
            SubscriberId = "subscriber043d4fa0518411e89a9d1bd8d0d9e684",
            Topics = new List<string> { "computer", "science" }
        });
    }
}
