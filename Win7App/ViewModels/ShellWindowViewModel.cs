using FirstFloor.ModernUI.Presentation;
using GalaSoft.MvvmLight;
using Win7Core.Base;
using Win7App.Properties;
using System;
using System.ComponentModel;

namespace Win7App.ViewModels
{
    public class ShellWindowViewModel : ViewModelBase
    {
        private static Link _settingsUri = new Link { DisplayName = "Settings", Source = new Uri("/Views/Settings.xaml", UriKind.Relative) };

        public AppLogger AppLogger { get; set; }

        private string _helpURL;
        public string HelpURL
        {
            get => _helpURL;
            set
            {
                Set(ref _helpURL, value);
                HelpUri = new Link { DisplayName = "Help", Source = new Uri(HelpURL, UriKind.Absolute) };
            }
        }

        private Link _helpUri;
        public Link HelpUri
        {
            get => _helpUri;
            set
            {
                Set(ref _helpUri, value);
                TitleLinks = new LinkCollection { _settingsUri, HelpUri };
            }
        }

        private LinkCollection _titleLinks = new LinkCollection();
        public LinkCollection TitleLinks
        {
            get => _titleLinks;
            set => Set(ref _titleLinks, value);
        }

        public string Title
        {
            get => Settings.Default.ApplicationFriendlyName;
        }

        public ShellWindowViewModel()
        {
            AppLogger = new AppLogger();

            HelpURL = Properties.Settings.Default.DefaultHelpURL;

            System.Windows.Application.Current.MainWindow.Closing += new CancelEventHandler(ShellWindow_Closing);
        }

        void ShellWindow_Closing(object sender, CancelEventArgs e)
        {
            GalaSoft.MvvmLight.Threading.DispatcherHelper.Reset();
        }
    }
}
