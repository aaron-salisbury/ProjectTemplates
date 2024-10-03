using System;
using System.Collections.Generic;

namespace WinXPApp.Views
{
    public partial class LogsView : Base.MVP.View
    {
        internal event EventHandler DownloadCommand;

        public LogsView()
        {
            InitializeComponent();
        }

        public void UpdateLogs(IList<string> logs)
        {
            //TODO: Would rather this be a list box or data grid.
            LogsTextBox.Text = string.Join(Environment.NewLine, [.. logs]);
        }

        private void DownloadButton_Click(object sender, EventArgs e)
        {
            if (DownloadCommand == null) { return; }

            DownloadCommand.Invoke(this, new EventArgs());
        }
    }
}
