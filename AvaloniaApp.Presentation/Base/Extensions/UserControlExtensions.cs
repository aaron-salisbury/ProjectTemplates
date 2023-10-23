using Avalonia.Controls;
using System;
using System.Reflection;

namespace AvaloniaApp.Presentation.Base.Extensions
{
    internal static class UserControlExtensions
    {
        internal static void SetDataContext(this UserControl view, IServiceProvider? services)
        {
            if (view != null)
            {
                Assembly currentAssembly = Assembly.GetExecutingAssembly();
                string viewType = view.GetType().ToString();

                if (currentAssembly != null && !string.IsNullOrEmpty(viewType) && viewType.EndsWith("View") && viewType.Contains(".Views."))
                {
                    string qualifiedViewModelPath = $"{viewType.Replace(".Views.", ".ViewModels.")}Model";
                    Type? viewModelType = currentAssembly.GetType(qualifiedViewModelPath);

                    if (viewModelType != null)
                    {
                        view.DataContext = services?.GetService(viewModelType);
                    }
                }
            }
        }
    }
}
