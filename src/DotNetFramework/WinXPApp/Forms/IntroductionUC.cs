using System;
using System.Windows.Forms;

namespace WinXPApp.Forms
{
    public partial class IntroductionUC : UserControl
    {
        public IntroductionUC()
        {
            InitializeComponent();
        }

        private void nlogLink_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://nlog-project.org/");
        }

        private void mfLink_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/peters/winforms-modernui");
        }

        private void mitLink_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/aaron-salisbury/ProjectTemplates/blob/master/LICENSE");
        }
    }
}
