using System;
using System.Reflection;
using System.Windows.Controls;

namespace Win7App.Base.Extensions;

internal static class UserControlExtensions
{
    internal static void SetDataContext(this UserControl view, IServiceProvider services)
    {
        if (view != null && services != null)
        {
            Assembly currentAssembly = Assembly.GetExecutingAssembly();
            string viewType = view.GetType().ToString();

            if (currentAssembly != null && !string.IsNullOrEmpty(viewType) && viewType.EndsWith("View") && viewType.Contains(".Views."))
            {
                string qualifiedViewModelPath = $"{viewType.Replace(".Views.", ".ViewModels.")}Model";
                Type viewModelType = currentAssembly.GetType(qualifiedViewModelPath);

                if (viewModelType != null)
                {
                    object viewModel = services.GetService(viewModelType);
                    view.DataContext = services?.GetService(viewModelType);
                }
            }
        }
    }
}
