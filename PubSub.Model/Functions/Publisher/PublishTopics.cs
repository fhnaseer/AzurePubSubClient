using System.Collections.Generic;
using PubSub.Model.Responses;

namespace PubSub.Model.Functions.Publisher
{
    public class PublishTopics : PostServerlessFunctionBase
    {
        public PublishTopics(string baseAddress) : base(baseAddress)
        {
        }

        public override string Name => "Publish Topics,";

        protected override string FunctionRelativeAddress => "publishtopic";

        private MessageBase _sampleMessageInput;
        public override MessageBase SampleMessageInput => _sampleMessageInput ?? (_sampleMessageInput = new TopicsInput
        {
            Message = "Some message,",
            Topics = new List<string> { "computer", "science" }
        });
    }
}