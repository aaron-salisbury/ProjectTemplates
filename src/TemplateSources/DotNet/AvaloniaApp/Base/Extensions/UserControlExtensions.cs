using Avalonia.Controls;
using Avalonia.Platform.Storage;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace AvaloniaApp.Presentation.Desktop.Base.Extensions;

internal static class UserControlExtensions
{
    internal static async Task<IStorageFile?> GetUserSelectedFileAsync(this UserControl view, string startingFolderPath, string title, params FilePickerFileType[] filePickerTypes)
    {
        IStorageFile? userSelectedFile = null;
        TopLevel? topLevel = TopLevel.GetTopLevel(view);

        if (topLevel != null)
        {
            FilePickerOpenOptions filePickerOpenOptions = new()
            {
                Title = title,
                AllowMultiple = false,
                SuggestedStartLocation = await topLevel.StorageProvider.TryGetFolderFromPathAsync(new Uri(startingFolderPath)),
                FileTypeFilter = filePickerTypes
            };

            IReadOnlyList<IStorageFile> files = await topLevel.StorageProvider.OpenFilePickerAsync(filePickerOpenOptions);

            if (files.Count > 0)
            {
                userSelectedFile = files[0];
            }
        }

        return userSelectedFile;
    }

    internal static async Task<IStorageFolder?> GetUserSelectedFolderAsync(this UserControl view, string startingFolderPath)
    {
        IStorageFolder? userSelectedFolder = null;
        TopLevel? topLevel = TopLevel.GetTopLevel(view);

        if (topLevel != null)
        {
            FolderPickerOpenOptions folderPickerOpenOptions = new()
            {
                AllowMultiple = false,
                SuggestedStartLocation = await topLevel.StorageProvider.TryGetFolderFromPathAsync(new Uri(startingFolderPath))
            };

            IReadOnlyList<IStorageFolder> folders = await topLevel.StorageProvider.OpenFolderPickerAsync(folderPickerOpenOptions);

            if (folders.Count > 0)
            {
                userSelectedFolder = folders[0];
            }
        }

        return userSelectedFolder;
    }

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

    internal static void LoadModelEvents(this UserControl view)
    {
        if (view.DataContext is BaseViewModel viewModel)
        {
            viewModel.AddModelEvents();
        }
    }

    internal static void UnloadModelEvents(this UserControl view)
    {
        if (view.DataContext is BaseViewModel viewModel)
        {
            viewModel.RemoveModelEvents();
        }
    }
}
