﻿using System.Windows.Forms;
using Win98App.Models;

namespace Win98App.Views
{
    public partial class HomeView : UserControl
    {
        public HomeView()
        {
            InitializeComponent();
        }

        internal void Initialize(HomeModel homeModel)
        {
            IntroLabel.Text = homeModel.IntroductionBlurb;
            LoggingLabel.Text = homeModel.LoggingBlurb;

            //TODO: Still need to get rid of the other text hard-coded in the view, but they're groups of labels. Need forms designer to test other controls.
        }

        private void LicenseLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/aaron-salisbury/ProjectTemplates/blob/master/license");
        }
    }
}
