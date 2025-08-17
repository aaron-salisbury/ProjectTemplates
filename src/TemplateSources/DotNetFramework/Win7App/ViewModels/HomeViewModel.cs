using Win7App.Base;
using Win7App.Properties;

namespace Win7App.ViewModels;

public class HomeViewModel : BaseViewModel
{
    public string Title
    {
        get { return Settings.Default.ApplicationFriendlyName; }
    }
}
