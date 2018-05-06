namespace PubSub.Model
{
    public class AzureContext
    {
        public AzureContext(string baseAddress)
        {
            BaseAddress = baseAddress;
        }

        public string BaseAddress { get; }

        private RegisterSubscriberFunction _registerSubscriberFunction;
        public RegisterSubscriberFunction RegisterSubscriberFunction => _registerSubscriberFunction ?? (_registerSubscriberFunction = new RegisterSubscriberFunction(BaseAddress));
    }
}
