using AvaloniaApp.Base;
using AvaloniaApp.Presentation.Desktop.Base;

namespace AvaloniaApp.Presentation.Desktop.ViewModels;

public partial class SettingsViewModel : BaseViewModel
{
    public string AppDisplayName { get; }

    public string ApplicationInfo { get; }

    public string CopyHolder { get; }

    public string AppDescription { get; }

    public string PrivacyURL { get; }

    public string IssuesURL { get; }

    public SettingsViewModel()
    {
        AppDisplayName = AppInfo.AppDisplayName;
        ApplicationInfo = $"{AppInfo.AppDisplayName} - {AppInfo.Version}";
        CopyHolder = AppInfo.CopyHolder;
        AppDescription = AppInfo.AppDescription;
        PrivacyURL = AppInfo.PrivacyURL;
        IssuesURL = AppInfo.IssuesURL;
    }
}
