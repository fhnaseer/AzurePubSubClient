using System.Collections.ObjectModel;
using System.Windows.Input;
using PubSub.Model;

namespace PubSub.Application.ViewModels
{
    public class CloudProviderViewModel : ViewModelBase
    {

        private ObservableCollection<CloudProvider> _providers;
        public ObservableCollection<CloudProvider> Providers => _providers ?? (_providers = new ObservableCollection<CloudProvider>(new[] { Model.CloudProvider.Azure, Model.CloudProvider.Aws }));

        private CloudProviderMetadata _cloudProvider;
        public CloudProviderMetadata CloudProvider
        {
            get => _cloudProvider;
            set
            {
                _cloudProvider = value;
                OnPropertyChanged(nameof(CloudProvider));
            }
        }


        private ICommand _launchServerlessCommand;
        public ICommand LaunchServerlessCommand => _launchServerlessCommand ?? (_launchServerlessCommand = new RelayCommand(LaunchServerless));

        internal void LaunchServerless()
        {
        }
    }
}
