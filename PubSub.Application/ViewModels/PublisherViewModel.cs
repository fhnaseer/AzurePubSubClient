namespace PubSub.Application.ViewModels
{
    public class PublisherViewModel : PubSubViewModelBase
    {
        public PublisherViewModel()
        {
            var functions = AzureContext.GetPublisherFunctions();
            foreach (var function in functions)
                Functions.Add(function);
        }
    }
}
