using System.Windows.Forms;

namespace Win98App.Forms
{
    public partial class IntroductionUC : UserControl
    {
        public IntroductionUC()
        {
            InitializeComponent();
        }

        private void LicenseLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/aaron-salisbury/ProjectTemplates/blob/master/LICENSE");
        }
    }
}
