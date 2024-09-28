using GalaSoft.MvvmLight.Ioc;
using Win7App.ViewModels.SampleTools;

namespace Win7App.ViewModels
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            SimpleIoc.Default.Register<ShellWindowViewModel>();
            SimpleIoc.Default.Register<IntroductionViewModel>();
            SimpleIoc.Default.Register<SettingsAppearanceViewModel>();
            SimpleIoc.Default.Register<LogViewModel>();

            // *** Sample tools ***
            SimpleIoc.Default.Register<UUIDGeneratorViewModel>();
            SimpleIoc.Default.Register<FlatUIColorPickerViewModel>();
            SimpleIoc.Default.Register<LineSorterViewModel>();
        }

        public ShellWindowViewModel ShellWindowViewModel { get { return SimpleIoc.Default.GetInstance<ShellWindowViewModel>(); } }
        public IntroductionViewModel IntroductionViewModel { get { return SimpleIoc.Default.GetInstance<IntroductionViewModel>(); } }
        public SettingsAppearanceViewModel SettingsAppearanceViewModel { get { return SimpleIoc.Default.GetInstance<SettingsAppearanceViewModel>(); } }
        public LogViewModel LogViewModel { get { return SimpleIoc.Default.GetInstance<LogViewModel>(); } }

        // *** Sample tools ***
        public UUIDGeneratorViewModel UUIDGeneratorViewModel { get { return SimpleIoc.Default.GetInstance<UUIDGeneratorViewModel>(); } }
        public FlatUIColorPickerViewModel FlatUIColorPickerViewModel { get { return SimpleIoc.Default.GetInstance<FlatUIColorPickerViewModel>(); } }
        public LineSorterViewModel LineSorterViewModel { get { return SimpleIoc.Default.GetInstance<LineSorterViewModel>(); } }
    }
}
