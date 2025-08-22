using AvaloniaApp.Base;
using RunnethOverStudio.AppToolkit.Presentation.MVVM;

namespace AvaloniaApp.ViewModels;

public partial class HomeViewModel : BaseViewModel
{
    public string AppDisplayName { get; }

    public string AppDescription { get; }

    public string LicenseURL { get; }

    public string DesignPatternBlurb { get; }

    public string AppearanceBlurb { get; }

    public HomeViewModel()
    {
        AppDisplayName = AppInfo.AppDisplayName;
        AppDescription = AppInfo.AppDescription;
        LicenseURL = AppInfo.LicenseURL;

        DesignPatternBlurb = "This solution follows a three-tier architecture, Data > Business > Presentation. " +
            "For the presentation layer, the MVVM design pattern is used. " +
            "MVVM separates views from models, which allows for projects that are cleaner, easier to extend, and testable. " +
            "Comes configured with structured logging using Serilog, distributed messaging using MassTransit, and an embedded SQLite database using Entity Framework.";

        AppearanceBlurb = "Avalonia uses a Fluent Design System that emphasizes modern, clean aesthetics, smooth animations, and intuitive interactions. " +
            "It provides a consistent and polished look-and-feel across different platforms, while giving developers flexibility with its styling system.";
    }
}
