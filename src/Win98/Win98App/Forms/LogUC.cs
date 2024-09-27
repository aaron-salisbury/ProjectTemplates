using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Win98Core.Base.Logging;

namespace Win98App.Forms
{
    public partial class LogUC : UserControl
    {
        public LogUC()
        {
            InitializeComponent();
        }

        public void UpdateLogs(IList<string> logs)
        {
            LogsTextBox.Text = string.Join(Environment.NewLine, ((List<string>)logs).ToArray());
        }

        private void DownloadButton_Click(object sender, EventArgs e)
        {
            AppLogger.OpenLog();
        }
    }
}
