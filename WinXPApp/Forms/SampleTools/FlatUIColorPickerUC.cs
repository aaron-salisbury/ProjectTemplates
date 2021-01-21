using MetroFramework.Controls;
using System;
using System.Drawing;
using System.Windows.Forms;
using WinXPCore.SampleTools;

namespace WinXPApp.Forms.SampleTools
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
            //TODO: Panel only changes size with its parent form at the bottom. It's supposed to towards the right as well.

            _flatUIColorPicker = new FlatUIColorPicker();
            _flatUIColorPicker.SetFlatColors();

            tlpColors.SuspendLayout();

            tlpColors.Controls.Clear();
            tlpColors.RowStyles.Clear();
            tlpColors.ColumnStyles.Clear();

            BuildColorTiles();

            tlpColors.AutoSize = true;
            tlpColors.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tlpColors.Anchor = (AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom);

            tlpColors.ResumeLayout(true);
        }

        private void BuildColorTiles()
        {
            tlpColors.RowCount = GetNumberOfRows(_flatUIColorPicker.FlatColors.Count);
            tlpColors.ColumnCount = Convert.ToInt32(Math.Ceiling((decimal)_flatUIColorPicker.FlatColors.Count / tlpColors.RowCount));

            float rowRercent = 100F / tlpColors.RowCount;
            float columnRercent = 100F / tlpColors.ColumnCount;

            for (int i = 0; i < tlpColors.RowCount; i++)
            {
                tlpColors.RowStyles.Add(new RowStyle(SizeType.Percent, rowRercent));
            }

            for (int i = 0; i < tlpColors.ColumnCount; i++)
            {
                tlpColors.RowStyles.Add(new RowStyle(SizeType.Percent, columnRercent));
            }

            int flatColorIndex = 0;
            for (int row = 0; row < tlpColors.RowCount; row++)
            {
                for (int column = 0; column < tlpColors.ColumnCount; column++)
                {
                    if (flatColorIndex < _flatUIColorPicker.FlatColors.Count)
                    {
                        FlatColor flatColor = _flatUIColorPicker.FlatColors[flatColorIndex++];

                        MetroTile colorTile = new MetroTile()
                        {
                            UseCustomBackColor = true,
                            BackColor = ColorTranslator.FromHtml(flatColor.Hex),
                            Tag = flatColor,
                            Margin = new Padding(1),
                            Anchor = (AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom)
                        };

                        colorTile.Click += new EventHandler(ColorTile_OnClick);

                        tlpColors.Controls.Add(colorTile);
                        tlpColors.SetRow(colorTile, row);
                        tlpColors.SetColumn(colorTile, column);
                    }
                }
            }
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
            MetroTile colorTile = (MetroTile)sender;
            FlatColor flatColor = (FlatColor)colorTile.Tag;

            txtName.Text = flatColor.Name;
            txtHex.Text = flatColor.Hex;
        }

        private void BtnCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(txtHex.Text ?? string.Empty);
        }
    }
}
