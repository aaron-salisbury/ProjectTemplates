using System;
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

            AppearanceManager.LoadBaseSettings(this);
            AppLogger.SetTargetInvoking(logUC1.UpdateLogs);
            logUC1.UpdateLogs(AppLogger.GetLogs()); // Load logs that may have been written before delegate could be set.

            // Sometimes the designer will regenerate the selected index of a tab control to be something different, but they should always initialize at 0.
            mainTabControl.SelectedIndex = 0;
            toolsTabControl.SelectedIndex = 0;
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
    }
}
