﻿using System.Threading.Tasks;

namespace PubSub.Model
{
    public class RegisterSubscriberFunction : ServerlessFunctionBase
    {
        public RegisterSubscriberFunction(string baseAddress)
        : base(baseAddress)
        {
        }

        public override string Name => "Register Subscriber,";

        protected override string FunctionRelativeAddress => "Subscribe";

        public override async Task<string> ExecuteFunction(object parameters)
        {
            return await HttpRestClient.Get(FunctionAddress);
        }
    }
}
