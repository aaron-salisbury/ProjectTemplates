using MetroFramework.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinXPApp.Base.Extensions;

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
            System.Diagnostics.Process.Start("https://mit-license.org/");
        }
    }
}
