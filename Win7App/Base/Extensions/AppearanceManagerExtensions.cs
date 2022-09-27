using FirstFloor.ModernUI.Presentation;
using System.Windows.Media;

namespace Win7App.Base.Extensions
{
    internal static class AppearanceManagerExtensions
    {
        private const string DARK_THEME = "Dark";
        private const string FONT_LARGE = "Large";

        internal static void LoadAppearancesFromSettings(this AppearanceManager appearanceManager, Properties.Settings settings)
        {
            appearanceManager.AccentColor = (Color)ColorConverter.ConvertFromString(settings.AccentColor);

            if (string.Equals(settings.ThemeSource, DARK_THEME))
            {
                appearanceManager.ThemeSource = AppearanceManager.DarkThemeSource;
            }
            else
            {
                appearanceManager.ThemeSource = AppearanceManager.LightThemeSource;
            }

            if (string.Equals(settings.FontSize, FONT_LARGE))
            {
                appearanceManager.FontSize = FontSize.Large;
            }
            else
            {
                appearanceManager.FontSize = FontSize.Small;
            }
        }
    }
}
