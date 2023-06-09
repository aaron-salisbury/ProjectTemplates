using AvaloniaApp.Base;
using CommunityToolkit.Mvvm.ComponentModel;
//using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace AvaloniaApp.ViewModels
{
    public partial class SettingsViewModel : ObservableObject
    {
        private const string SETTINGS_FILE_NAME = "settings.json";

        [ObservableProperty]
        private bool _isDarkSelected;

        [ObservableProperty]
        private double? _backgroundOpacity;

        private Settings? _appSettings;
        public Settings? AppSettings
        {
            get
            {
                return _appSettings;
            }
            set
            {
                _appSettings = value;
                if (BackgroundOpacity != value?.BackgroundOpacity)
                {
                    BackgroundOpacity = value?.BackgroundOpacity;
                }
            }
        }

        public string? AppDirectoryPath { get; set; }

        public SettingsViewModel()
        {
            AppDirectoryPath = GetAppDirectoryPath();

            Load();

            _isDarkSelected = string.Equals(AppSettings?.ThemeMode, "Dark", StringComparison.OrdinalIgnoreCase);
        }

        public void Load()
        {
            Settings defaultSettings = new Settings()
            { 
                ThemeMode = "Dark",
                BackgroundOpacity = 1.0D
            };

            string filePath = Path.Combine(AppDirectoryPath ?? GetAppDirectoryPath(), SETTINGS_FILE_NAME);

            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                Settings? loadedSettings = JsonSerializer.Deserialize<Settings>(json);
                AppSettings = loadedSettings ?? defaultSettings;
            }
            else
            {
                AppSettings = defaultSettings;
            }
        }

        public void Save()
        {
            if (AppSettings != null)
            {
                // Sync the stored object with the observable properties.
                AppSettings.ThemeMode = IsDarkSelected ? "Dark" : "Light";
                AppSettings.BackgroundOpacity = BackgroundOpacity;

                FileInfo file = new FileInfo(Path.Combine(AppDirectoryPath ?? GetAppDirectoryPath(), SETTINGS_FILE_NAME));

                if (file != null && file.Directory != null)
                {
                    file.Directory.Create();
                    string json = JsonSerializer.Serialize(AppSettings);

                    File.WriteAllText(file.FullName, json);
                }
            }
        }

        private static string GetAppDirectoryPath()
        {
            string appPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string appName = App.Current?.Name != null ? string.Concat(App.Current.Name.Where(c => !char.IsWhiteSpace(c))) : "temp";

            return Path.Combine(appPath, appName);
        }
    }
}
