using AvaloniaApp.Presentation.Base;

namespace AvaloniaApp.Presentation.ViewModels
{
    public partial class IntroductionViewModel : BaseViewModel
    {
        public string AppDisplayName { get; }

        public string AppDescription { get; }

        public string LicenseURL { get; }

        public IntroductionViewModel()
        {
            AppDisplayName = AppInfo.AppDisplayName;
            AppDescription = AppInfo.AppDescription;
            LicenseURL = AppInfo.LicenseURL;
        }
    }
}
