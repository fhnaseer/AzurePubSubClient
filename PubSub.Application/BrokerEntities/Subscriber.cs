using System;
using System.Text;
using Newtonsoft.Json;
using PubSub.Model.Responses;

namespace PubSub.Application.BrokerEntities
{
    public abstract class Subscriber : BrokerEntity
    {
        public SubscribeResponse SubscribeResponse { get; set; }

        public string SubscriberId
        {
            get => SubscribeResponse.SubscriberId;
            set => SubscribeResponse.SubscriberId = value;
        }

        public abstract void SetupMessageQueue();

        internal async void RegisterSubscriber()
        {
            var responseString = await CloudContext.RegisterSubscriber.ExecuteFunction(null);
            AppendText(responseString);
            try
            {
                SubscribeResponse = JsonConvert.DeserializeObject<SubscribeResponse>(responseString);
                OnPropertyChanged(nameof(SubscriberId));
                SetupMessageQueue();
            }
            catch (Exception)
            {
            }
        }

        internal async void UnregisterSubscriber()
        {
            var message = CloudContext.UnregisterSubscriber.SampleMessageInput;
            message.SubscriberId = SubscriberId;
            var response = await CloudContext.UnregisterSubscriber.ExecuteFunction(message);
            AppendText(response);
        }
    }
}
