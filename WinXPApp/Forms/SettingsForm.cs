using MetroFramework;
using MetroFramework.Drawing;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WinXPApp.Base;

namespace WinXPApp.Forms
{
    public partial class SettingsForm : BaseForm
    {
        private ShellForm _shellForm;

        public SettingsForm(ShellForm shellForm)
        {
            InitializeComponent();

            _shellForm = shellForm;

            LoadThemesComboBox();




            LoadStylesList();
        }

        private void LoadThemesComboBox()
        {
            // Temporarily disable SelectedIndexChanged so theme setting doesn't get triggered.
            themeComboBox.SelectedIndexChanged -= new EventHandler(themeComboBox_SelectedIndexChanged);

            // Get light & dark selectable theme options. "Default" doesn't make sense to select in this context.
            themeComboBox.DataSource = Enum.GetValues(typeof(MetroThemeStyle))
                .Cast<MetroThemeStyle>()
                .Where(item => item != MetroThemeStyle.Default)
                .ToArray();

            themeComboBox.SelectedItem = Theme;

            themeComboBox.SelectedIndexChanged += new EventHandler(themeComboBox_SelectedIndexChanged);
        }

        private void themeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            MetroThemeStyle newTheme = (MetroThemeStyle)themeComboBox.SelectedItem;

            AppearanceManager.SetThemeOnForms(newTheme, this, _shellForm);

            AppearanceManager.SaveSettings(_shellForm.BaseMetroStyleManager);
        }

        private void LoadStylesList()
        {
            List<MetroColorStyle> colorStyles = Enum.GetValues(typeof(MetroColorStyle)).Cast<MetroColorStyle>().ToList();
            colorStyles.RemoveAt(0); // Remove default.

            int topPanelOffset = 0;
            int middlePanelOffset = 0;
            int bottomPanelOffset = 0;

            for (int i = 0; i < colorStyles.Count; i++)
            {
                Panel panelToAddTo;
                int offset;

                if (i < 5)
                {
                    offset = topPanelOffset++;
                    panelToAddTo = stylesPanelTop;
                }
                else if (i > 9)
                {
                    offset = bottomPanelOffset++;
                    panelToAddTo = stylesPanelBottom;
                }
                else
                {
                    offset = middlePanelOffset++;
                    panelToAddTo = stylesPanelMiddle;
                }

                Button styleButton = new Button()
                {
                    Size = new Size(35, 35),
                    Enabled = true,
                    UseVisualStyleBackColor = false,
                    Top = 20,
                    Left = (offset * 40),
                    Margin = new Padding(0),
                    Padding = new Padding(0),
                    BackColor = MetroPaint.GetStyleColor(colorStyles[i]),
                    ForeColor = MetroPaint.GetStyleColor(colorStyles[i]),
                };

                styleButton.Click += new EventHandler(styleButton_Click);

                panelToAddTo.Controls.Add(styleButton);
            }
        }

        private void styleButton_Click(object sender, EventArgs e)
        {
            MetroColorStyle? style = AppearanceManager.ConvertStyleToColor(((Button)sender).BackColor);

            if (style != null)
            {
                AppearanceManager.SetStyleOnForms(style.Value, this, _shellForm);

                AppearanceManager.SaveSettings(_shellForm.BaseMetroStyleManager);
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
