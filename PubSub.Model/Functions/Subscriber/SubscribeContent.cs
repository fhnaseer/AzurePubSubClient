using System.Collections.Generic;
using System.Threading.Tasks;
using PubSub.Model.Responses;

namespace PubSub.Model.Functions.Subscriber
{
    public class SubscribeContent : ServerlessFunctionBase
    {
        public SubscribeContent(string baseAddress)
            : base(baseAddress)
        {
        }

        public override string Name => "Subscribe Content,";

        protected override string FunctionRelativeAddress => "SubscribeContent";

        public override async Task<string> ExecuteFunction(object parameters)
        {
            return await HttpRestClient.Post(FunctionAddress, parameters);
        }

        public override MessageBase SampleMessageInput => new SubscribeContentInput
        {
            SubscriberId = "subscriber043d4fa0518411e89a9d1bd8d0d9e684",
            Topics = new List<KeyValueContent>
            {
                new KeyValueContent { Key = "computer", Value = "intel", Condition = "="},
                new KeyValueContent { Key = "money", Value = "500", Condition = ">="}
            }
        };
    }
}
