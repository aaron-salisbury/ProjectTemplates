using System;
using System.Windows.Forms;
using WinXPCore.SampleTools;

namespace WinXPApp.Forms
{
    public partial class UUIDGeneratorUC : UserControl
    {
        private UUIDGenerator _uuidGenerator { get; set; }

        public UUIDGeneratorUC()
        {
            InitializeComponent();

            _uuidGenerator = new UUIDGenerator();
        }

        private void BtnGenerate_Click(object sender, EventArgs e)
        {
            _uuidGenerator.Capitalize = cbCapitalize.Checked;
            _uuidGenerator.Initiate();

            txtNewUUID.Text = _uuidGenerator.UUID;
        }

        private void BtnCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(_uuidGenerator.UUID ?? string.Empty);
        }
    }
}
