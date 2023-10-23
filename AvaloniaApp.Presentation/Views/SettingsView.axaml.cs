using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using AvaloniaApp.Presentation.Base.Extensions;
using AvaloniaApp.Presentation.Base;
using AvaloniaApp.Presentation.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace AvaloniaApp.Presentation.Views
{
    public partial class SettingsView : UserControl
    {
        public SettingsView()
        {
            InitializeComponent();
            this.SetDataContext(App.Current?.Services);
        }

        private void PrivacyBtn_OnClick(object? sender, RoutedEventArgs a)
        {
            if (DataContext is SettingsViewModel viewModel)
            {
                Browser.Open(viewModel.PrivacyURL);
            }
        }

        private void IssuesBtn_OnClick(object? sender, RoutedEventArgs a)
        {
            if (DataContext is SettingsViewModel viewModel)
            {
                Browser.Open(viewModel.IssuesURL);
            }
        }

        private async void SelectFolderBtn_OnClick(object? sender, RoutedEventArgs a)
        {
            if (DataContext is SettingsViewModel viewModel)
            {
                IStorageFolder? folder = await GetUserSelectedFolderAsync(viewModel.StorageFolderPath);

                if (folder != null)
                {
                    string folderPath = folder.Path.AbsolutePath;

                    if (!string.IsNullOrEmpty(folderPath))
                    {
                        viewModel.StorageFolderPath = folderPath;
                    }
                }
            }
        }

        private async Task<IStorageFolder?> GetUserSelectedFolderAsync(string startingFolderPath)
        {
            IStorageFolder? userSelectedFolder = null;
            TopLevel? topLevel = TopLevel.GetTopLevel(this);

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
    }
}
