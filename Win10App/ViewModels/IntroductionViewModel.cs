using Win10App.Base.Extensions;

namespace Win10App.ViewModels
{
    public class IntroductionViewModel : BaseViewModel
    {
        private string _appDisplayName;
        public string AppDisplayName
        {
            get => _appDisplayName;
            set => SetProperty(ref _appDisplayName, value);
        }

        public IntroductionViewModel()
        {
            AppDisplayName = "AppDisplayName".GetLocalized();
        }
    }
}
