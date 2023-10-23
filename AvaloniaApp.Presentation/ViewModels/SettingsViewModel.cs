using AvaloniaApp.Business;
using AvaloniaApp.Presentation.Base;
using System.Threading.Tasks;

namespace AvaloniaApp.Presentation.ViewModels
{
    public partial class SettingsViewModel : BaseViewModel
    {
        private string _storageFolderPath = Manager.GetCurrentUserStorageDirectory();
        public string StorageFolderPath
        {
            get { return _storageFolderPath; }
            set
            {
                SetProperty(ref _storageFolderPath, value);
                Task.Run(() => Manager.UpdateUserStorageDirectoryAsync(_storageFolderPath));
            }
        }

        public string AppDisplayName { get; }

        public string ApplicationInfo { get; }

        public string CopyHolder { get; }

        public string AppDescription { get; }

        public string PrivacyURL { get; }

        public string IssuesURL { get; }

        public SettingsViewModel()
        {
            AppDisplayName = AppInfo.AppDisplayName;
            ApplicationInfo = $"{AppInfo.AppDisplayName} - {Manager.AppVersion}";
            CopyHolder = AppInfo.CopyHolder;
            AppDescription = AppInfo.AppDescription;
            PrivacyURL = AppInfo.PrivacyURL;
            IssuesURL = AppInfo.IssuesURL;
        }
    }
}
