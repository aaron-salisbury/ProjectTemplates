using System;
using System.Windows.Forms;
using Win98Core.SampleTools;

namespace Win98App.Forms.SampleTools
{
    public partial class UUIDGeneratorUC : UserControl
    {
        private UUIDGenerator _uuidGenerator { get; set; }

        public UUIDGeneratorUC()
        {
            InitializeComponent();
        }

        private void UUIDGeneratorUC_Load(object sender, EventArgs e)
        {
            _uuidGenerator = new UUIDGenerator();
        }

        private void GenerateButton_Click(object sender, EventArgs e)
        {
            _uuidGenerator.Capitalize = CapitalizeCheckBox.Checked;
            _uuidGenerator.Initiate();

            NewGuidTextBox.Text = _uuidGenerator.UUID;
        }

        private void CopyButton_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(_uuidGenerator.UUID ?? string.Empty);
        }
    }
}
