using PubSub.Model;

namespace PubSub.Application.ViewModels
{
    public abstract class SubscriberViewModelBase : PubSubViewModelBase
    {
        public SubscriberViewModelBase(CloudProviderMetadata cloudProvider) : base(cloudProvider)
        {
            Functions.Clear();
            var functions = CloudContext.GetSubscriberFunctions();
            foreach (var function in functions)
                Functions.Add(function);
        }

        internal SubscriberViewModelBase() : base(null) { }

        internal async void RegisterSubscriber()
        {
            var responseString = await CloudContext.RegisterSubscriber.ExecuteFunction(null);
            AppendText(responseString);
            OnRegisterSubscriber(responseString);
        }

        protected abstract void OnRegisterSubscriber(string response);

        internal async void UnregisterSubscriber()
        {
            var message = CloudContext.UnregisterSubscriber.SampleMessageInput;
            message.SubscriberId = SubscriberId;
            var response = await CloudContext.UnregisterSubscriber.ExecuteFunction(message);
            AppendText(response);
        }

        private string _subscriberId;
        public string SubscriberId
        {
            get => _subscriberId;
            set
            {
                _subscriberId = value;
                OnPropertyChanged(nameof(SubscriberId));
            }
        }

        public override async void ExecuteFunction()
        {
            SelectedFunction.SampleMessageInput.SubscriberId = SubscriberId;
            var response = await SelectedFunction.ExecuteFunction(SelectedFunction.SampleMessageInput);
            AppendText(response);
        }
    }
}
