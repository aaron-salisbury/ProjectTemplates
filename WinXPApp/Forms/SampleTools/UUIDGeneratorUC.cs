using System;
using System.Windows.Forms;
using WinXPCore.SampleTools;

namespace WinXPApp.Forms
{
    public partial class UUIDGeneratorUC : UserControl
    {
        private UUIDGenerator _uuidGenerator { get; set; }
        private ErrorProvider _uuidErrorProvider;

        public UUIDGeneratorUC()
        {
            InitializeComponent();
        }

        private void UUIDGeneratorUC_Load(object sender, EventArgs e)
        {
            _uuidGenerator = new UUIDGenerator();

            _uuidErrorProvider = new ErrorProvider
            {
                BlinkStyle = ErrorBlinkStyle.NeverBlink
            };

            //txtNewUUID.DataBindings.Add(new Binding(nameof(TextBox.Text), _uuidGenerator, _uuidGenerator.UUID));
            //txtNewUUID.DataBindings.Add(nameof(TextBox.Text), _uuidGenerator, _uuidGenerator.UUID, true, DataSourceUpdateMode.OnPropertyChanged, string.Empty);
            //txtNewUUID.DataBindings.Add(new Binding(nameof(TextBox.Text), _uuidGenerator, _uuidGenerator.UUID, true, DataSourceUpdateMode.OnPropertyChanged, string.Empty));
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

        //TODO: Try to replace manual validating with binding.
        private void SetAndValidateUUID()
        {
            NewGuidTextBox.Text = _uuidGenerator.UUID;

            string errorMessage = _uuidGenerator[nameof(UUIDGenerator.UUID)];

            if (!string.IsNullOrEmpty(errorMessage))
            {
                _uuidErrorProvider.SetError(NewGuidTextBox, errorMessage);
            }
            else
            {
                _uuidErrorProvider.SetError(NewGuidTextBox, string.Empty);
            }
        }
    }
}
