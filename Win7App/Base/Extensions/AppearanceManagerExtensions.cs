using FirstFloor.ModernUI.Presentation;
using System.Windows.Media;

namespace Win7App.Base.Extensions
{
    internal static class AppearanceManagerExtensions
    {
        internal static void LoadAppearancesFromSettings(this AppearanceManager appearanceManager, Properties.Settings settings)
        {
            appearanceManager.AccentColor = (Color)ColorConverter.ConvertFromString(settings.AccentColor);

            if (string.Equals(settings.ThemeSource, nameof(AppearanceManager.DarkThemeSource).GetFirstWord()))
            {
                appearanceManager.ThemeSource = AppearanceManager.DarkThemeSource;
            }
            else
            {
                appearanceManager.ThemeSource = AppearanceManager.LightThemeSource;
            }

            if (string.Equals(settings.FontSize, nameof(FontSize.Large)))
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
