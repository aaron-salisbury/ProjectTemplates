using System;
using System.Windows.Forms;
using Win98App.Forms;

namespace Win98App
{
    public partial class ShellForm : Form
    {
        private readonly Padding _defaultContentAreaPadding = new Padding(15);

        public ShellForm()
        {
            InitializeComponent();

            Text = Properties.Settings.Default.ApplicationFriendlyName;

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

        private void AboutMenuItem_Click(object sender, EventArgs e)
        {
            using (AboutForm aboutForm = new AboutForm())
            {
                aboutForm.ShowDialog(this);
            }
        }
    }
}
