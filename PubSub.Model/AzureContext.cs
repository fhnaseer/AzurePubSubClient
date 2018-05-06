namespace PubSub.Model
{
    public class AzureContext
    {
        private readonly string _baseAddress;
        public AzureContext(string baseAddress)
        {
            _baseAddress = baseAddress;
        }

        private RegisterSubscriberFunction _registerSubscriberFunction;
        public RegisterSubscriberFunction RegisterSubscriberFunction => _registerSubscriberFunction ?? (_registerSubscriberFunction = new RegisterSubscriberFunction(_baseAddress));
    }
}
