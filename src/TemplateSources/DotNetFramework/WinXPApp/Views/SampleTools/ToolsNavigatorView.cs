using DotNetFramework.Core.ComponentModel;
using System.ComponentModel;
using System.Drawing;
using WinXPApp.Base;

namespace WinXPApp.Views.SampleTools
{
    public partial class ToolsNavigatorView : Base.MVP.View
    {
        public ToolsNavigatorView()
        {
            InitializeComponent();

            ToolsNavigator.SelectedIndex = 0;
            ConfigureStaticNavDesign();

            ObservableObject.StaticPropertyChanged += HandleGlobalPropertyChanged;
        }

        void HandleGlobalPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // The KryptonNavigator control is not a part of the MetroModernUI package
            // so it doesn't respond to app style changes. Manually doing so here.
            if (string.Equals(e.PropertyName, "CurrentStyle") || string.Equals(e.PropertyName, "CurrentTheme"))
            {
                ConfigureDynamicNavDesign();
            }
        }

        private void ConfigureStaticNavDesign()
        {
            ToolsNavigator.StateNormal.HeaderGroup.Border.Draw = ComponentFactory.Krypton.Toolkit.InheritBool.True;
            ToolsNavigator.StateNormal.HeaderGroup.Border.DrawBorders = ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left;

            ToolsNavigator.StateNormal.Page.Draw = ComponentFactory.Krypton.Toolkit.InheritBool.False;
            ToolsNavigator.StateNormal.Panel.Draw = ComponentFactory.Krypton.Toolkit.InheritBool.False;

            ToolsNavigator.StateDisabled.Page.Draw = ComponentFactory.Krypton.Toolkit.InheritBool.False;
            ToolsNavigator.StateDisabled.Panel.Draw = ComponentFactory.Krypton.Toolkit.InheritBool.False;
        }

        private void ConfigureDynamicNavDesign()
        {
            ToolsNavigator.StateNormal.RibbonTab.TabDraw.TextColor = AppearanceManager.CurrentTheme == MetroFramework.MetroThemeStyle.Dark
                ? ColorTranslator.FromHtml("#bdc3c7")
                : Color.Black;

            Color accentColor = MetroFramework.Drawing.MetroPaint.GetStyleColor(AppearanceManager.CurrentStyle);
            Color foreColor = ColorIsCloserToBlackThanWhite(accentColor) ? Color.White : Color.Black;

            ToolsNavigator.StateNormal.HeaderGroup.Border.Color1 = accentColor;
            ToolsNavigator.StateNormal.HeaderGroup.Border.Color2 = accentColor;

            ToolsNavigator.StateSelected.RibbonTab.TabDraw.BackColor1 = accentColor;
            ToolsNavigator.StateSelected.RibbonTab.TabDraw.BackColor2 = accentColor;
            ToolsNavigator.StateSelected.RibbonTab.TabDraw.BackColor3 = accentColor;
            ToolsNavigator.StateSelected.RibbonTab.TabDraw.BackColor4 = accentColor;
            ToolsNavigator.StateSelected.RibbonTab.TabDraw.BackColor5 = accentColor;
            ToolsNavigator.StateSelected.RibbonTab.TabDraw.TextColor = foreColor;

            ToolsNavigator.StateTracking.RibbonTab.TabDraw.BackColor1 = accentColor;
            ToolsNavigator.StateTracking.RibbonTab.TabDraw.BackColor2 = accentColor;
            ToolsNavigator.StateTracking.RibbonTab.TabDraw.BackColor3 = accentColor;
            ToolsNavigator.StateTracking.RibbonTab.TabDraw.BackColor4 = accentColor;
            ToolsNavigator.StateTracking.RibbonTab.TabDraw.BackColor5 = accentColor;
            ToolsNavigator.StateTracking.RibbonTab.TabDraw.TextColor = foreColor;

            ToolsNavigator.OverrideFocus.RibbonTab.TabDraw.BackColor1 = accentColor;
            ToolsNavigator.OverrideFocus.RibbonTab.TabDraw.BackColor2 = accentColor;
            ToolsNavigator.OverrideFocus.RibbonTab.TabDraw.BackColor3 = accentColor;
            ToolsNavigator.OverrideFocus.RibbonTab.TabDraw.BackColor4 = accentColor;
            ToolsNavigator.OverrideFocus.RibbonTab.TabDraw.BackColor5 = accentColor;
            ToolsNavigator.OverrideFocus.RibbonTab.TabDraw.TextColor = foreColor;
        }

        private static bool ColorIsCloserToBlackThanWhite(Color color)
        {
            // https://stackoverflow.com/questions/9780632/how-do-i-determine-if-a-color-is-closer-to-white-or-black

            double y = 0.2126 * color.R + 0.7152 * color.G + 0.0722 * color.B;

            return y < 128D;
        }
    }
}
