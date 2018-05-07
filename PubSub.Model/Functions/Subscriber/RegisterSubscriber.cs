﻿using System.Threading.Tasks;
using PubSub.Model.Responses;

namespace PubSub.Model.Functions.Subscriber
{
    public class RegisterSubscriber : ServerlessFunctionBase
    {
        public RegisterSubscriber(string baseAddress)
        : base(baseAddress)
        {
        }

        public override string Name => "Register Subscriber,";

        protected override string FunctionRelativeAddress => "Subscribe";

        public override async Task<string> ExecuteFunction(object parameters)
        {
            return await HttpRestClient.Get(FunctionAddress);
        }

        public override MessageBase SampleMessageInput => new MessageBase();
    }
}