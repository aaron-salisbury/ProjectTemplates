using System;
using System.Windows.Forms;
using Win98App.Forms;
using Win98Core.Base;

namespace Win98App
{
    public partial class ShellForm : Form
    {
        private readonly Padding _defaultContentAreaPadding = new Padding(15);
        private readonly LogUC _logUC;

        public ShellForm()
        {
            InitializeComponent();

            Text = Properties.Settings.Default.ApplicationFriendlyName;

            _logUC = new LogUC();
            _logUC.Padding = _defaultContentAreaPadding;
            _logUC.Dock = DockStyle.Fill;
            AppLogger.SetTargetInvoking(_logUC.UpdateLogs);
            _logUC.UpdateLogs(AppLogger.GetLogs()); // Load logs that may have been written before delegate could be set.

            MainContentPanel.Controls.Add(GetHomeContent());
        }

        private Control GetHomeContent()
        {
            Control homeContent = new IntroductionUC();
            homeContent.Padding = _defaultContentAreaPadding;
            homeContent.Dock = DockStyle.Fill;

            return homeContent;
        }

        private void HomeMenuItem_Click(object sender, EventArgs e)
        {
            MainContentPanel.Controls.Clear();
            MainContentPanel.Controls.Add(GetHomeContent());
        }

        private void ExitMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void UUIDGeneratorMenuItem_Click(object sender, EventArgs e)
        {
            MainContentPanel.Controls.Clear();

            Control generator = new Forms.SampleTools.UUIDGeneratorUC();
            generator.Padding = _defaultContentAreaPadding;
            MainContentPanel.Controls.Add(generator);
        }

        private void FlatUIColorPickerMenuItem_Click(object sender, EventArgs e)
        {
            MainContentPanel.Controls.Clear();

            Control colorPicker = new Forms.SampleTools.FlatUIColorPickerUC();
            colorPicker.Padding = _defaultContentAreaPadding;
            colorPicker.Dock = DockStyle.Fill;
            MainContentPanel.Controls.Add(colorPicker);
        }

        private void LineSorterMenuItem_Click(object sender, EventArgs e)
        {
            MainContentPanel.Controls.Clear();

            Control lineSorter = new Forms.SampleTools.LineSorterUC();
            lineSorter.Padding = _defaultContentAreaPadding;
            lineSorter.Dock = DockStyle.Fill;
            MainContentPanel.Controls.Add(lineSorter);
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
