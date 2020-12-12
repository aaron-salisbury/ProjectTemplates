using Win7App.Properties;

namespace Win7App.ViewModels
{
    public class IntroductionViewModel : BaseViewModel
    {
        public string Title
        {
            get => Settings.Default.ApplicationFriendlyName;
        }
    }
}
