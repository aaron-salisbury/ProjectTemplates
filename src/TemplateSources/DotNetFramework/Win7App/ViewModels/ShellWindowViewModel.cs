using DotNetFrameworkToolkit.Modules.ComponentModel;
using FirstFloor.ModernUI.Presentation;
using System;
using Win7App.Properties;

namespace Win7App.ViewModels;

public class ShellWindowViewModel : ObservableObject
{
    private static readonly Link _settingsUri = new() { DisplayName = "Settings", Source = new Uri("/Views/Settings.xaml", UriKind.Relative) };

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
            TitleLinks = [_settingsUri, HelpUri];
        }
    }

    private LinkCollection _titleLinks = [];
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
    }
}
