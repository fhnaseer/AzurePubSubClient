using System.Globalization;
using System.Threading.Tasks;
using PubSub.Model.Responses;

namespace PubSub.Model.Functions
{
    public abstract class ServerlessFunctionBase
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

        public abstract MessageBase SampleMessageInput { get; }
    }

    public abstract class GetServerlessFunctionBase : ServerlessFunctionBase
    {
        protected GetServerlessFunctionBase(string baseAddress) : base(baseAddress)
        {
        }

        public override async Task<string> ExecuteFunction(object parameters)
        {
            return await HttpRestClient.Get(FunctionAddress);
        }
    }

    public abstract class PostServerlessFunctionBase : ServerlessFunctionBase
    {
        protected PostServerlessFunctionBase(string baseAddress) : base(baseAddress)
        {
        }

        public override async Task<string> ExecuteFunction(object parameters)
        {
            return await HttpRestClient.Post(FunctionAddress, parameters);
        }
    }
}
