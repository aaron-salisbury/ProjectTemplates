using System;
using System.Collections.Generic;
using System.Windows.Forms;
using WinXPCore.Base;

namespace WinXPApp.Forms
{
    public partial class LogUC : UserControl
    {
        public LogUC()
        {
            InitializeComponent();
        }

        public void UpdateLogs(IList<string> logs)
        {
            tbLogs.Text = string.Join(Environment.NewLine, ((List<string>)logs).ToArray());
        }

        private void BtnDownload_Click(object sender, System.EventArgs e)
        {
            AppLogger.OpenLog();
        }
    }
}
