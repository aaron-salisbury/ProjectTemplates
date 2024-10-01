using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Win98App.Views
{
    public partial class LogsView : UserControl
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
