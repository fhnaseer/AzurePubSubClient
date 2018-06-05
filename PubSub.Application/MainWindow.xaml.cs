using PubSub.Application.ViewModels;

namespace PubSub.Application
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new CloudProviderViewModel();
        }
    }
}
