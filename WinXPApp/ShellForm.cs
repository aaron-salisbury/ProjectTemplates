using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using WinXPApp.Base;
using WinXPApp.Forms;
using WinXPCore.Base;

namespace WinXPApp
{
    public partial class ShellForm : BaseForm
    {
        public ShellForm()
        {
            InitializeComponent();

            Text = Properties.Settings.Default.ApplicationFriendlyName;

            AppearanceManager.LoadBaseSettings(this);
            AppLogger.SetTargetInvoking(logUC1.UpdateLogs);
            logUC1.UpdateLogs(AppLogger.GetLogs()); // Load logs that may have been written before delegate could be set.

            // Sometimes the designer will regenerate the selected index of a tab control to be something different, but they should always initialize at 0.
            mainTabControl.SelectedIndex = 0;
        }

        private void SettingsLink_Click(object sender, EventArgs e)
        {
            SettingsForm settingsForm = new SettingsForm(this);
            settingsForm.Activated += (o, ea) => { Enabled = false; };
            settingsForm.FormClosed += (o, fce) => { Enabled = true; };
            settingsForm.Show();
        }

        private void HelpLink_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Properties.Settings.Default.HelpLink);
        }


        private IEnumerable<Control> GetAllControlsByTag(Control control, string tag)
        {
            IEnumerable<Control> controls = control.Controls.Cast<Control>();

            return controls.SelectMany(ctrl => GetAllControlsByTag(ctrl, tag))
                .Concat(controls)
                .Where(c => string.Equals(c.Tag, tag));
        }
    }
}
