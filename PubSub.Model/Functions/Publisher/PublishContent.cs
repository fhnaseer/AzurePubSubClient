﻿using System.Collections.Generic;
using System.Threading.Tasks;
using PubSub.Model.Responses;

namespace PubSub.Model.Functions.Publisher
{
    public class PublishContent : ServerlessFunctionBase
    {
        public PublishContent(string baseAddress) : base(baseAddress)
        {
        }

        public override string Name => "Publish Content,";

        protected override string FunctionRelativeAddress => "PublishContent";

        public override async Task<string> ExecuteFunction(object parameters)
        {
            return await HttpRestClient.Post(FunctionAddress, parameters);
        }

        private MessageBase _sampleMessageInput;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "PubSub.Model.Responses.ContentsInput.set_Message(System.String)")]
        public override MessageBase SampleMessageInput => _sampleMessageInput ?? (_sampleMessageInput = new ContentsInput
        {
            Message = "Some message,",
            Topics = new List<KeyValueContent>
            {
                new KeyValueContent { Key = "computer", Value = "intel"},
                new KeyValueContent { Key = "money", Value = "1000"}
            }
        });
    }
}