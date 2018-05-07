using System.Threading.Tasks;
using PubSub.Model.Responses;

namespace PubSub.Model.Functions.Publisher
{
    public class PublishFunction : ServerlessFunctionBase
    {
        public PublishFunction(string baseAddress)
            : base(baseAddress)
        {
        }

        public override string Name => "Publish Function,";

        protected override string FunctionRelativeAddress => "PublishFunction";

        public override async Task<string> ExecuteFunction(object parameters)
        {
            return await HttpRestClient.Post(FunctionAddress, parameters);
        }

        public override MessageBase SampleMessageInput => new FunctionInput
        {
            Message = "Germany",
            SubscriptionType = "Text",
        };
    }
}
