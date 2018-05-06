using System.Threading.Tasks;

namespace PubSub.Model
{
    public interface IServerlessFunction
    {
        string Name { get; }

        string FunctionAddress { get; }

        Task<string> ExecuteFunction(object parameters);
    }
}
