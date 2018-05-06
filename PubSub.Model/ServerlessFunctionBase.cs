﻿using System.Globalization;
using System.Threading.Tasks;

namespace PubSub.Model
{
    public abstract class ServerlessFunctionBase : IServerlessFunction
    {
        private readonly string _baseAddress;
        protected ServerlessFunctionBase(string baseAddress)
        {
            _baseAddress = baseAddress;
        }

        public abstract string Name { get; }

        protected abstract string FunctionRelativeAddress { get; }

        public string FunctionAddress => string.Format(CultureInfo.CurrentCulture, "{0}/api/{1}", _baseAddress, FunctionRelativeAddress);

        public abstract Task<string> ExecuteFunction(object parameters);
    }
}
