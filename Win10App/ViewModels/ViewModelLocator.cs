using GalaSoft.MvvmLight.Ioc;
using Win10App.Base.Services;
using Win10App.Views;

namespace Win10App.ViewModels
{
    [Windows.UI.Xaml.Data.Bindable]
    public class ViewModelLocator
    {
        private static ViewModelLocator _current;
        public static ViewModelLocator Current => _current ?? (_current = new ViewModelLocator());

        public ViewModelLocator()
        {
            SimpleIoc.Default.Register(() => new NavigationServiceEx());
            SimpleIoc.Default.Register<ShellViewModel>();
            Register<SettingsViewModel, SettingsPage>();
            Register<IntroductionViewModel, IntroductionPage>();
            Register<LogViewModel, LogPage>();

            // *** Sample tools ***
            Register<ToolsViewModel, ToolsPage>();
            Register<UUIDGeneratorViewModel, UUIDGeneratorPage>();
            Register<FlatUIColorPickerViewModel, FlatUIColorPickerPage>();
            Register<LineSorterViewModel, LineSorterPage>();
        }

        public NavigationServiceEx NavigationService => SimpleIoc.Default.GetInstance<NavigationServiceEx>();
        public ShellViewModel ShellViewModel => SimpleIoc.Default.GetInstance<ShellViewModel>();
        public SettingsViewModel SettingsViewModel => SimpleIoc.Default.GetInstance<SettingsViewModel>();
        public IntroductionViewModel IntroductionViewModel => SimpleIoc.Default.GetInstance<IntroductionViewModel>();
        public LogViewModel LogViewModel => SimpleIoc.Default.GetInstance<LogViewModel>();

        // *** Sample tools ***
        public ToolsViewModel ToolsViewModel => SimpleIoc.Default.GetInstance<ToolsViewModel>();
        public UUIDGeneratorViewModel UUIDGeneratorViewModel => SimpleIoc.Default.GetInstance<UUIDGeneratorViewModel>();
        public FlatUIColorPickerViewModel FlatUIColorPickerViewModel => SimpleIoc.Default.GetInstance<FlatUIColorPickerViewModel>();
        public LineSorterViewModel LineSorterViewModel => SimpleIoc.Default.GetInstance<LineSorterViewModel>();

        public void Register<VM, V>() where VM : class
        {
            SimpleIoc.Default.Register<VM>();

            NavigationService.Configure(typeof(VM).FullName, typeof(V));
        }
    }
}
