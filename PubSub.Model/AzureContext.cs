using System.Collections.Generic;
using PubSub.Model.Functions;
using PubSub.Model.Functions.Publisher;
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

        private RegisterSubscriber _registerSubscriber;
        public RegisterSubscriber RegisterSubscriber => _registerSubscriber ?? (_registerSubscriber = new RegisterSubscriber(BaseAddress));


        private SubscribeTopic _subscribeTopic;
        private SubscribeTopic SubscribeTopic => _subscribeTopic ?? (_subscribeTopic = new SubscribeTopic(BaseAddress));


        private SubscribeContent _subscribeContent;
        private SubscribeContent SubscribeContent => _subscribeContent ?? (_subscribeContent = new SubscribeContent(BaseAddress));


        private SubscribeFunctions _subscribeFunctions;
        public SubscribeFunctions SubscribeFunctions => _subscribeFunctions ?? (_subscribeFunctions = new SubscribeFunctions(BaseAddress));

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        public IList<ServerlessFunctionBase> GetSubscriberFunctions()
        {
            return new List<ServerlessFunctionBase>
            {
                SubscribeTopic,
                SubscribeContent,
                SubscribeFunctions
            };
        }

        private PublishTopics _publishTopics;
        public PublishTopics PublishTopics => _publishTopics ?? (_publishTopics = new PublishTopics(BaseAddress));

        private PublishContent _publishContent;
        public PublishContent PublishContent => _publishContent ?? (_publishContent = new PublishContent(BaseAddress));

        private PublishFunction _publishFunction;
        public PublishFunction PublishFunction => _publishFunction ?? (_publishFunction = new PublishFunction(BaseAddress));

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        public IList<ServerlessFunctionBase> GetPublisherFunctions()
        {
            return new List<ServerlessFunctionBase>
            {
                PublishTopics,
                PublishContent,
                PublishFunction
            };
        }
    }
}
