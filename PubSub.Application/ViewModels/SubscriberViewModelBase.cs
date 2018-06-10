using Newtonsoft.Json;
using PubSub.Model;
using PubSub.Model.Responses;

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
            Subscriber = JsonConvert.DeserializeObject<SubscribeResponse>(responseString);
            SetupMessageQueue();
        }

        protected abstract void SetupMessageQueue();

        internal async void UnregisterSubscriber()
        {
            var message = CloudContext.UnregisterSubscriber.SampleMessageInput;
            message.SubscriberId = Subscriber.SubscriberId;
            var response = await CloudContext.UnregisterSubscriber.ExecuteFunction(message);
            AppendText(response);
        }

        private SubscribeResponse _subscriber;
        public SubscribeResponse Subscriber
        {
            get => _subscriber;
            set
            {
                _subscriber = value;
                OnPropertyChanged(nameof(Subscriber));
            }
        }

        public override async void ExecuteFunction()
        {
            SelectedFunction.SampleMessageInput.SubscriberId = Subscriber.SubscriberId;
            var response = await SelectedFunction.ExecuteFunction(SelectedFunction.SampleMessageInput);
            AppendText(response);
        }
    }
}
