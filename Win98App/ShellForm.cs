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

            MainContentPanel.Controls.Add(new IntroductionUC { Padding = _defaultContentAreaPadding });
        }

        private void HomeMenuItem_Click(object sender, EventArgs e)
        {
            MainContentPanel.Controls.Clear();
            MainContentPanel.Controls.Add(new IntroductionUC { Padding = _defaultContentAreaPadding });
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
            MainContentPanel.Controls.Add(new Forms.SampleTools.FlatUIColorPickerUC { Padding = _defaultContentAreaPadding });
        }

        private void LineSorterMenuItem_Click(object sender, EventArgs e)
        {
            MainContentPanel.Controls.Clear();
            MainContentPanel.Controls.Add(new Forms.SampleTools.LineSorterUC { Padding = _defaultContentAreaPadding });
        }
    }
}
