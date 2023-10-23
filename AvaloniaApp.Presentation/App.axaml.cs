using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using AvaloniaApp.Presentation.Base.Services;
using AvaloniaApp.Presentation.Views;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Reflection;

namespace AvaloniaApp.Presentation;

public partial class App : Application
{
    public new static App? Current => Application.Current as App;

    public IServiceProvider? Services { get; set; }

    public override void Initialize()
    {
        Services = ConfigureServices();

        Business.Manager.AppVersion = GetAppVersion();
        Business.Manager.HttpClientFactory = (IHttpClientFactory?)Services.GetService(typeof(IHttpClientFactory));

        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        // Line below is needed to remove Avalonia data validation.
        // Without this line you will get duplicate validations from both Avalonia and CT
        BindingPlugins.DataValidators.RemoveAt(0);

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow();
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleView)
        {
            singleView.MainView = new MainView();
        }

        base.OnFrameworkInitializationCompleted();
    }

    private static IServiceProvider ConfigureServices()
    {
        // https://docs.microsoft.com/en-us/windows/communitytoolkit/mvvm/ioc
        ServiceCollection services = new();

        // https://learn.microsoft.com/en-us/dotnet/core/extensions/httpclient-factory
        services.AddHttpClient();

        services.AddScoped(typeof(IAgnosticDispatcher), typeof(AvaloniaDispatcher));

        // This app requires the naming convention that views end in "View",
        // view models end in "ViewModel", and that nothing else ends in either.
        // See Base.ViewLocator.cs and Base.Extensions.UserControlExtensions.SetDataContext().
        foreach (Type appType in Assembly.GetExecutingAssembly().GetTypes())
        {
            if (appType.Name.EndsWith("ViewModel"))
            {
                services.AddSingleton(appType);
            }
        }

        return services.BuildServiceProvider();
    }

    private static string GetAppVersion()
    {
        string versionNumber = string.Empty;

        // Version set in Project File (right-click project) > Package > Package Version
        Assembly executingAssembly = Assembly.GetExecutingAssembly();
        AssemblyName assemblyName = executingAssembly.GetName();

        if (assemblyName.Version != null)
        {
            versionNumber = assemblyName.Version.ToString();

            // Drop 4th position, only care about Major.Minor.Patch
            int index = versionNumber.LastIndexOf(".");
            if (index >= 0)
            {
                versionNumber = versionNumber.Substring(0, index);
            }
        }

        return versionNumber;
    }
}
