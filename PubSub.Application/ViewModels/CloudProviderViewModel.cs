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
            get => _cloudProvider ?? (_cloudProvider = new CloudProviderMetadata { BaseAddress = "http://localhost:7071", PublishersCount = 1, SubscribersCount = 1 });
            set
            {
                _cloudProvider = value;
                OnPropertyChanged(nameof(CloudProvider));
            }
        }

        private ICommand _launchServerlessCommand;
        public ICommand LaunchServerlessCommand => _launchServerlessCommand ?? (_launchServerlessCommand = new RelayCommand(LaunchServerless, CanLaunchServerless));

        internal bool CanLaunchServerless() { return CloudProvider.BaseAddress != null && CloudProvider.PublishersCount != 0 && CloudProvider.SubscribersCount != 0; }

        internal void LaunchServerless()
        {
            var window = new MainWindow();
            window.DataContext = new HomeViewModel(CloudProvider);
            window.Show();
        }
    }
}
