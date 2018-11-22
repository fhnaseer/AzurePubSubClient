using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PubSub.Application.BrokerEntities;
using PubSub.Model;

namespace PubSub.Application.ViewModels
{
    public class PublisherViewModel : PubSubViewModelBase
    {
        public PublisherViewModel(ConfigurationFile configurationFile) : base(configurationFile)
        {
            var functions = CloudContext.GetPublisherFunctions();
            foreach (var function in functions)
                Functions.Add(function);
        }

        private Publisher _publisher;
        public Publisher Publisher => _publisher ?? (_publisher = new Publisher());

        internal PublisherViewModel() : base(null) { }

        public int PublicationsCount => ConfigurationFile.NodesCount;

        public override async void ExecuteFunction()
        {
            var taskList = new List<Task>();
            var message = SelectedFunction.SampleMessageInput;
            var startTime = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff");
            message.FromPublisher = startTime;
            for (var i = 0; i < PublicationsCount; i++)
                taskList.Add(SelectedFunction.ExecuteFunction(message));

            await Task.WhenAll(taskList);
            var endTime = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff");
            var start = Convert.ToDateTime(startTime);
            var end = Convert.ToDateTime(endTime);
            var total = (end - start).TotalMilliseconds;
            Publisher.AppendText($"Total Time Taken: {total}");
        }
    }
}
