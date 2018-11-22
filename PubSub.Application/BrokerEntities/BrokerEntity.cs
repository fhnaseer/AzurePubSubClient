using System.Text;
using PubSub.Application.ViewModels;

namespace PubSub.Application.BrokerEntities
{
    public class BrokerEntity : ViewModelBase
    {
        private readonly StringBuilder _messageResponses = new StringBuilder();

        public string MessageResponses => _messageResponses.ToString();

        public void AppendText(string nextLine)
        {
            _messageResponses.AppendLine(nextLine);
            OnPropertyChanged(nameof(MessageResponses));
        }
    }
}
