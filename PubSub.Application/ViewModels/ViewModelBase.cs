using System.ComponentModel;
using System.Runtime.CompilerServices;
using PubSub.Model;

namespace PubSub.Application.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        private CloudContext _azureContext;
        public CloudContext AzureContext
        {
            get => _azureContext ?? (_azureContext = new CloudContext("http://localhost:7071"));
            set => _azureContext = value;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1026:DefaultParametersShouldNotBeUsed")]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
