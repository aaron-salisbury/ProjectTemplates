using System;
using System.Windows.Forms;
using Win98App.Base.Helpers;
using Win98Core.SampleTools;

namespace Win98App.Forms.SampleTools
{
    public partial class UUIDGeneratorUC : UserControl
    {
        private UUIDGenerator _uuidGenerator;
        private StandardErrorProvider _errorProvider;

        public UUIDGeneratorUC()
        {
            InitializeComponent();
        }

        private void UUIDGeneratorUC_Load(object sender, EventArgs e)
        {
            _uuidGenerator = new UUIDGenerator();
            _errorProvider = new StandardErrorProvider();
        }

        private void GenerateButton_Click(object sender, EventArgs e)
        {
            _uuidGenerator.Capitalize = CapitalizeCheckBox.Checked;
            _uuidGenerator.Initiate();

            SetAndValidateUUID();
        }

        private void CopyButton_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(_uuidGenerator.UUID ?? string.Empty);
        }

        private void SetAndValidateUUID()
        {
            NewGuidTextBox.Text = _uuidGenerator.UUID;

            _uuidGenerator.Validate();
            _errorProvider.UpdateError(NewGuidTextBox, _uuidGenerator.ErrorMessage);
        }
    }
}
