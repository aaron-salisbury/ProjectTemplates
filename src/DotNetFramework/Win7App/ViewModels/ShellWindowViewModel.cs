using DotNetFramework.Core.ComponentModel;
using FirstFloor.ModernUI.Presentation;
using System;
using System.ComponentModel;
using Win7App.Properties;

namespace Win7App.ViewModels
{
    public class ShellWindowViewModel : ObservableObject
    {
        private static readonly Link _settingsUri = new Link { DisplayName = "Settings", Source = new Uri("/Views/Settings.xaml", UriKind.Relative) };

        private string _helpURL;
        public string HelpURL
        {
            get { return _helpURL; }
            set
            {
                SetField(ref _helpURL, value, nameof(HelpURL));
                HelpUri = new Link { DisplayName = "Help", Source = new Uri(HelpURL, UriKind.Absolute) };
            }
        }

        private Link _helpUri;
        public Link HelpUri
        {
            get { return _helpUri; }
            set
            {
                SetField(ref _helpUri, value, nameof(HelpUri));
                TitleLinks = new LinkCollection { _settingsUri, HelpUri };
            }
        }

        private LinkCollection _titleLinks = new LinkCollection();
        public LinkCollection TitleLinks
        {
            get { return _titleLinks; }
            set { SetField(ref _titleLinks, value, nameof(TitleLinks)); }
        }

        public string Title
        {
            get { return Settings.Default.ApplicationFriendlyName; }
        }

        public ShellWindowViewModel()
        {
            HelpURL = Properties.Settings.Default.DefaultHelpURL;

            System.Windows.Application.Current.MainWindow.Closing += new CancelEventHandler(ShellWindow_Closing);
        }

        void ShellWindow_Closing(object sender, CancelEventArgs e)
        {
            GalaSoft.MvvmLight.Threading.DispatcherHelper.Reset();
        }
    }
}
