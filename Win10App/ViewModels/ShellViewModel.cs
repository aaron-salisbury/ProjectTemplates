using Win10Core.Base;
using Windows.UI.Xaml;

namespace Win10App.ViewModels
{
    public class ShellViewModel : BaseNavigableViewModel
    {
        public static Visibility IsDebug
        {
#if DEBUG
            get { return Visibility.Visible; }
#else
            get { return Visibility.Collapsed; }
#endif
        }

        public AppLogger AppLogger { get; set; }

        public ShellViewModel()
        {
            AppLogger = new AppLogger();
        }
    }
}
