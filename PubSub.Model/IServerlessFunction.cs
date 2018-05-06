namespace PubSub.Model
{
    public interface IServerlessFunction
    {
        string Name { get; set; }

        string FunctionAddress { get; set; }

        object ExecuteFunction(object parameters);
    }
}
