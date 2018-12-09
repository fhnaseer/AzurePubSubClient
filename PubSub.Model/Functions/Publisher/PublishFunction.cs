using PubSub.Model.Responses;

namespace PubSub.Model.Functions.Publisher
{
    public class PublishFunction : PostServerlessFunctionBase
    {
        public PublishFunction(string baseAddress)
            : base(baseAddress)
        {
        }

        public override string Name => "Publish Function";

        protected override string FunctionRelativeAddress => "publishfunction";

        public override MessageBase SampleMessageInput => new PublishFunctionInput
        {
            Message = "Welcome",
        };
    }
}
