﻿using MetroFramework;
using MetroFramework.Components;
using MetroFramework.Drawing;
using System;
using System.Drawing;
using WinXPApp.Forms;
using WinXPCore.Base;

namespace WinXPApp.Base
{
    public class AppearanceManager : ObservableObject
    {
        private static MetroThemeStyle _currentTheme;
        public static MetroThemeStyle CurrentTheme
        {
            get => _currentTheme;
            set { _currentTheme = value; StaticRaisePropertyChanged(nameof(CurrentTheme)); }
                
        }

        private static MetroColorStyle _currentStyle;
        public static MetroColorStyle CurrentStyle
        {
            get => _currentStyle;
            set { _currentStyle = value; StaticRaisePropertyChanged(nameof(CurrentStyle)); }

        }

        public static void LoadBaseSettings(BaseForm form)
        {
            SetThemeOnForms(Properties.Settings.Default.Theme, form);
            SetStyleOnForms(Properties.Settings.Default.Style, form);
        }

        public static void SaveSettings(MetroStyleManager styleManager)
        {
            Properties.Settings.Default.Style = styleManager.Style;
            Properties.Settings.Default.Theme = styleManager.Theme;

            Properties.Settings.Default.Save();
        }

        public static void SetStyleOnForms(MetroColorStyle style, params BaseForm[] forms)
        {
            CurrentStyle = style;

            foreach (BaseForm form in forms)
            {
                form.Style = style;
                form.BaseMetroStyleManager.Style = style;
            }
        }

        public static void SetThemeOnForms(MetroThemeStyle theme, params BaseForm[] forms)
        {
            CurrentTheme = theme;

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
