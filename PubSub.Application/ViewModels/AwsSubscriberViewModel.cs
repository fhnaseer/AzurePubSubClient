using PubSub.Model;

namespace PubSub.Application.ViewModels
{
    public class AwsSubscriberViewModel : SubscriberViewModelBase
    {
        public AwsSubscriberViewModel(CloudProviderMetadata cloudProvider) : base(cloudProvider)
        {
            Functions.Clear();
            var functions = CloudContext.GetSubscriberFunctions();
            foreach (var function in functions)
                Functions.Add(function);
        }

        internal AwsSubscriberViewModel() : base(null) { }

        protected override void OnRegisterSubscriber(string response)
        {
            //var subscribeResponse = JsonConvert.DeserializeObject<SubscribeResponse>(response);
            //SubscriberId = subscribeResponse.QueueName;
            //ConnectionString = subscribeResponse.ConnectionString;
            //SetupMessageQueue(subscribeResponse);
        }
    }
}
