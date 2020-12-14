using MetroFramework;
using MetroFramework.Components;
using MetroFramework.Drawing;
using System;
using System.Drawing;
using WinXPApp.Forms;

namespace WinXPApp.Base
{
    public class AppearanceManager
    {
        public static void LoadBaseSettings(BaseForm form)
        {
            SetStyleOnForms(Properties.Settings.Default.Style, form);
            SetThemeOnForms(Properties.Settings.Default.Theme, form);
        }

        public static void SaveSettings(MetroStyleManager styleManager)
        {
            Properties.Settings.Default.Style = styleManager.Style;
            Properties.Settings.Default.Theme = styleManager.Theme;

            Properties.Settings.Default.Save();
        }

        public static void SetStyleOnForms(MetroColorStyle style, params BaseForm[] forms)
        {
            foreach (BaseForm form in forms)
            {
                form.Style = style;
                form.BaseMetroStyleManager.Style = style;
            }
        }

        public static void SetThemeOnForms(MetroThemeStyle theme, params BaseForm[] forms)
        {
            foreach (BaseForm form in forms)
            {
                form.Theme = theme;
                form.BaseMetroStyleManager.Theme = theme;
            }
        }

        public static MetroColorStyle? ConvertStyleToColor(Color color)
        {
            foreach (MetroColorStyle style in Enum.GetValues(typeof(MetroColorStyle)))
            {
                if (color == MetroPaint.GetStyleColor(style))
                {
                    return style;
                }
            }

            return null;
        }
    }
}
