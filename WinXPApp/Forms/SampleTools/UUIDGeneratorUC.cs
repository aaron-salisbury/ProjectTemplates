using System;
using System.Windows.Forms;
using WinXPApp.Base.Helpers;
using WinXPCore.SampleTools;

namespace WinXPApp.Forms
{
    public partial class UUIDGeneratorUC : UserControl
    {
        private UUIDGenerator _uuidGenerator { get; set; }
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

        private void BtnGenerate_Click(object sender, EventArgs e)
        {
            _uuidGenerator.Capitalize = cbCapitalize.Checked;
            _uuidGenerator.Initiate();

            SetAndValidateUUID();
        }

        private void BtnCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(_uuidGenerator.UUID ?? string.Empty);
        }

        private void SetAndValidateUUID()
        {
            NewGuidTextBox.Text = _uuidGenerator.UUID;

            string potentialErrorMessage = _uuidGenerator[nameof(UUIDGenerator.UUID)];
            _errorProvider.UpdateError(NewGuidTextBox, potentialErrorMessage);
        }
    }
}
