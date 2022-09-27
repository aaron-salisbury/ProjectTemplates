using System;
using System.Drawing;
using System.Windows.Forms;

namespace Win98App.Forms
{
    public partial class AboutForm : Form
    {
        private const int MODAL_WIDTH = 525;
        private const int MODAL_HIGHT = 325;

        public AboutForm()
        {
            InitializeComponent();

            SetupAboutPage();
        }

        private void SetupAboutPage()
        {
            Size modalSize = new Size(MODAL_WIDTH, MODAL_HIGHT);
            Size = modalSize;
            MinimumSize = modalSize;
            MaximumSize = modalSize;
            SizeGripStyle = SizeGripStyle.Hide;

            Text = string.Format("About {0}", Properties.Settings.Default.ApplicationFriendlyName);
            AppNameLabel.Text = Properties.Settings.Default.ApplicationFriendlyName;
            VersionLabel.Text = string.Format("Version {0}", Application.ProductVersion);
            AppDescriptionLabel.Text = Properties.Settings.Default.AboutDescription;
        }

        private void AppLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(Properties.Settings.Default.ApplicationLink);
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
