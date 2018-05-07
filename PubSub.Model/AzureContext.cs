using PubSub.Model.Functions.Subscriber;

namespace PubSub.Model
{
    public class AzureContext
    {
        public AzureContext(string baseAddress)
        {
            BaseAddress = baseAddress;
        }

        public string BaseAddress { get; }

        private RegisterSubscriber _registerSubscriberFunction;
        public RegisterSubscriber RegisterSubscriberFunction => _registerSubscriberFunction ?? (_registerSubscriberFunction = new RegisterSubscriber(BaseAddress));
    }
}
