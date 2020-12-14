using NLog;
using System;
using System.Windows.Forms;
using WinXPApp.Base;
using WinXPApp.Forms;
using WinXPCore.Base;

namespace WinXPApp
{
    public partial class ShellForm : BaseForm
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public ShellForm()
        {
            InitializeComponent();

            FormClosing += new FormClosingEventHandler(ShellForm_FormClosing);

            AppLogger.SetTargetInvoking(logUC1.UpdateLogs);
            AppearanceManager.LoadBaseSettings(this);

            for(int i = 1; i < 101; i++)
            {
                _logger.Info($"Test {i}");
            }
        }

        private void SettingsLink_Click(object sender, EventArgs e)
        {
            SettingsForm settingsForm = new SettingsForm(this);
            settingsForm.Activated += (o, ae) => { Enabled = false; };
            settingsForm.FormClosed += (o, fce) => { Enabled = true; };
            settingsForm.Show();
        }

        private void HelpLink_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Properties.Settings.Default.HelpLink);
        }

        private void ShellForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //TODO: Might need this in the future.
        }
    }
}
