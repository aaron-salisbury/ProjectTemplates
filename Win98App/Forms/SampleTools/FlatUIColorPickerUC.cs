using System;
using System.Drawing;
using System.Windows.Forms;
using Win98Core.SampleTools;

namespace Win98App.Forms.SampleTools
{
    public partial class FlatUIColorPickerUC : UserControl
    {
        private FlatUIColorPicker _flatUIColorPicker { get; set; }

        public FlatUIColorPickerUC()
        {
            InitializeComponent();

            SetupColorPanel();
        }

        private void SetupColorPanel()
        {
            ColorsTLP.Controls.Clear();
            ColorsTLP.RowStyles.Clear();
            ColorsTLP.ColumnStyles.Clear();
            ColorsTLP.AutoSize = true;
            ColorsTLP.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ColorsTLP.Anchor = (AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom);

            BuildColorTiles();
        }

        private void BuildColorTiles()
        {
            ColorsTLP.SuspendLayout();

            _flatUIColorPicker = new FlatUIColorPicker();
            _flatUIColorPicker.SetFlatColors();

            ColorsTLP.RowCount = GetNumberOfRows(_flatUIColorPicker.FlatColors.Count);
            ColorsTLP.ColumnCount = Convert.ToInt32(Math.Ceiling((decimal)_flatUIColorPicker.FlatColors.Count / ColorsTLP.RowCount));

            float rowRercent = 100F / ColorsTLP.RowCount;
            float columnRercent = 100F / ColorsTLP.ColumnCount;

            for (int i = 0; i < ColorsTLP.RowCount; i++)
            {
                ColorsTLP.RowStyles.Add(new RowStyle(SizeType.Percent, rowRercent));
            }

            for (int i = 0; i < ColorsTLP.ColumnCount; i++)
            {
                ColorsTLP.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, columnRercent));
            }

            int flatColorIndex = 0;
            for (int row = 0; row < ColorsTLP.RowCount; row++)
            {
                for (int column = 0; column < ColorsTLP.ColumnCount; column++)
                {
                    if (flatColorIndex < _flatUIColorPicker.FlatColors.Count)
                    {
                        FlatColor flatColor = _flatUIColorPicker.FlatColors[flatColorIndex++];

                        Button colorTile = new Button()
                        {
                            BackColor = ColorTranslator.FromHtml(flatColor.Hex),
                            Tag = flatColor,
                            Margin = new Padding(1),
                            Anchor = (AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom)
                        };

                        colorTile.Click += new EventHandler(ColorTile_OnClick);

                        ColorsTLP.Controls.Add(colorTile);
                        ColorsTLP.SetRow(colorTile, row);
                        ColorsTLP.SetColumn(colorTile, column);
                    }
                }
            }

            ColorsTLP.ResumeLayout(true);
        }

        private static int GetNumberOfRows(int totalItemCount)
        {
            int numRows = totalItemCount;
            while (numRows % 2 == 0)
            {
                numRows /= 2;
            }

            return numRows;
        }

        private void ColorTile_OnClick(object sender, EventArgs e)
        {
            Button colorTile = (Button)sender;
            FlatColor flatColor = (FlatColor)colorTile.Tag;

            NameTextBox.Text = flatColor.Name;
            HexTextBox.Text = flatColor.Hex;
        }

        private void CopyButton_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(HexTextBox.Text ?? string.Empty);
        }
    }
}
