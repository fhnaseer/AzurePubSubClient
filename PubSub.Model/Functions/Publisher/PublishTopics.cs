using System.Collections.Generic;
using System.Threading.Tasks;
using PubSub.Model.Responses;

namespace PubSub.Model.Functions.Publisher
{
    public class PublishTopics : ServerlessFunctionBase
    {
        public PublishTopics(string baseAddress) : base(baseAddress)
        {
        }

        public override string Name => "Publish Topics,";

        protected override string FunctionRelativeAddress => "PublishTopic";

        public override async Task<string> ExecuteFunction(object parameters)
        {
            return await HttpRestClient.Post(FunctionAddress, parameters);
        }

        private MessageBase _sampleMessageInput;
        public override MessageBase SampleMessageInput => _sampleMessageInput ?? (_sampleMessageInput = new TopicsInput
        {
            Message = "Some message,",
            Topics = new List<string> { "computer", "science" }
        });
    }
}