using AvaloniaApp.Base;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;

namespace AvaloniaApp.ViewModels
{
    public partial class SettingsViewModel : ObservableObject
    {
        [ObservableProperty]
        private bool _isDarkSelected;

        public Settings? AppSettings { get; set; }

        public SettingsViewModel()
        {
            Load();
            _isDarkSelected = string.Equals(AppSettings?.ThemeMode, "Dark", StringComparison.OrdinalIgnoreCase);
        }

        public void Load()
        {
            Settings defaultSettings = new Settings() { ThemeMode = "Dark" };
            string filePath = GetSettingsFilePath();

            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                Settings loadedSettings = JsonConvert.DeserializeObject<Settings>(json);
                AppSettings = loadedSettings?.ThemeMode != null ? loadedSettings : defaultSettings;
            }
            else
            {
                AppSettings = defaultSettings;
            }
        }

        public void Save()
        {
            FileInfo file = new FileInfo(GetSettingsFilePath());

            if (file != null && file.Directory != null)
            {
                file.Directory.Create();
                string json = JsonConvert.SerializeObject(AppSettings);

                File.WriteAllText(file.FullName, json);
            }
        }

        private static string GetSettingsFilePath()
        {
            string appPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string appName = App.Current?.Name != null ? string.Concat(App.Current.Name.Where(c => !char.IsWhiteSpace(c))) : "temp";
            string directoryPath = Path.Combine(appPath, appName);

            return Path.Combine(directoryPath, "settings.json");
        }
    }
}
