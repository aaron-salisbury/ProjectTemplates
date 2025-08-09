using System;
using System.Windows.Forms;
using WinXPApp.Base.Helpers;

namespace WinXPApp.Views.SampleTools
{
    public partial class UUIDGeneratorView : Base.MVP.View
    {
        internal event EventHandler<GenerateCommandEventArgs> GenerateCommand;

        private StandardErrorProvider _errorProvider;

        public UUIDGeneratorView()
        {
            InitializeComponent();
        }

        internal void UUIDGenerated(string uUID)
        {
            NewGuidTextBox.Text = uUID;

            //TODO: Validate.
            //string potentialErrorMessage = _uuidGenerator["UUID"];
            //_errorProvider.UpdateError(NewGuidTextBox, potentialErrorMessage);
        }

        private void UUIDGeneratorUC_Load(object sender, EventArgs e)
        {
            _errorProvider = new StandardErrorProvider();
        }

        private void GenerateButton_Click(object sender, EventArgs e)
        {
            if (GenerateCommand == null) { return; }

            GenerateCommand.Invoke(this, new GenerateCommandEventArgs()
            {
                ShouldCapitalize = cbCapitalize.Checked
            });
        }

        private void CopyButton_Click(object sender, EventArgs e)
        {
            if (NewGuidTextBox != null && !string.IsNullOrEmpty(NewGuidTextBox.Text))
            {
                Clipboard.SetText(NewGuidTextBox.Text);
            }
        }
    }

    public class GenerateCommandEventArgs : EventArgs
    {
        public bool ShouldCapitalize { get; set; }
    }
}
