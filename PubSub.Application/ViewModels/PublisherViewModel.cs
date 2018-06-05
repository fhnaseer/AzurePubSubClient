﻿using PubSub.Model;

namespace PubSub.Application.ViewModels
{
    public class PublisherViewModel : PubSubViewModelBase
    {
        public PublisherViewModel(CloudProviderMetadata cloudProvider) : base(cloudProvider)
        {
            var functions = CloudContext.GetPublisherFunctions();
            foreach (var function in functions)
                Functions.Add(function);
        }

        internal PublisherViewModel() : base(null) { }
    }
}
