using System;
using System.Windows.Forms;
using Win98App.Forms;
using Win98Core.Base;

namespace Win98App
{
    public partial class ShellForm : Form
    {
        private readonly Padding _defaultContentAreaPadding = new Padding(15);
        private LogUC _logUC;

        public ShellForm()
        {
            InitializeComponent();

            Text = Properties.Settings.Default.ApplicationFriendlyName;

            _logUC = new LogUC { Padding = _defaultContentAreaPadding, Dock = DockStyle.Fill };
            AppLogger.SetTargetInvoking(_logUC.UpdateLogs);
            _logUC.UpdateLogs(AppLogger.GetLogs()); // Load logs that may have been written before delegate could be set.

            MainContentPanel.Controls.Add(new IntroductionUC { Padding = _defaultContentAreaPadding, Dock = DockStyle.Fill });
        }

        private void HomeMenuItem_Click(object sender, EventArgs e)
        {
            MainContentPanel.Controls.Clear();
            MainContentPanel.Controls.Add(new IntroductionUC { Padding = _defaultContentAreaPadding, Dock = DockStyle.Fill });
        }

        private void ExitMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void UUIDGeneratorMenuItem_Click(object sender, EventArgs e)
        {
            MainContentPanel.Controls.Clear();
            MainContentPanel.Controls.Add(new Forms.SampleTools.UUIDGeneratorUC { Padding = _defaultContentAreaPadding });
        }

        private void FlatUIColorPickerMenuItem_Click(object sender, EventArgs e)
        {
            MainContentPanel.Controls.Clear();
            MainContentPanel.Controls.Add(new Forms.SampleTools.FlatUIColorPickerUC { Padding = _defaultContentAreaPadding, Dock = DockStyle.Fill });
        }

        private void LineSorterMenuItem_Click(object sender, EventArgs e)
        {
            MainContentPanel.Controls.Clear();
            MainContentPanel.Controls.Add(new Forms.SampleTools.LineSorterUC { Padding = _defaultContentAreaPadding, Dock = DockStyle.Fill });
        }

        private void LogMenuItem_Click(object sender, EventArgs e)
        {
            MainContentPanel.Controls.Clear();
            MainContentPanel.Controls.Add(_logUC);
        }

        private void AboutMenuItem_Click(object sender, EventArgs e)
        {
            using (AboutForm aboutForm = new AboutForm())
            {
                aboutForm.ShowDialog(this);
            }
        }
    }
}
