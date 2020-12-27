using Win10Core.Base;

namespace Win10App.ViewModels
{
    public class ShellViewModel : BaseNavigableViewModel
    {
        public AppLogger AppLogger { get; set; }

        public ShellViewModel()
        {
            AppLogger = new AppLogger();
        }
    }
}
