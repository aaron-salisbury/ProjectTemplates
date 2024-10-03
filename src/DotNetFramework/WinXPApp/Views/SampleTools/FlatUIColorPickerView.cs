using DotNetFramework.Business.Modules.Sample.DTOs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace WinXPApp.Views.SampleTools
{
    public partial class FlatUIColorPickerView : Base.MVP.View
    {
        private List<FlatColorDto> _flatColors;

        public FlatUIColorPickerView()
        {
            InitializeComponent();
        }

        internal void Initialize(List<FlatColorDto> flatColors)
        {
            _flatColors = flatColors;

            SetupColorPanel();
        }

        private void ColorTile_Click(object sender, EventArgs e)
        {
            Button colorTile = (Button)sender;
            FlatColorDto flatColor = (FlatColorDto)colorTile.Tag;

            NameTextBox.Text = flatColor.Name;
            HexTextBox.Text = flatColor.Hex;
        }

        private void CopyButton_Click(object sender, EventArgs e)
        {
            if (HexTextBox != null && !string.IsNullOrEmpty(HexTextBox.Text))
            {
                Clipboard.SetText(HexTextBox.Text);
            }
        }

        #region Tile Building
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

            ColorsTLP.RowCount = GetNumberOfRows(_flatColors.Count);
            ColorsTLP.ColumnCount = Convert.ToInt32(Math.Ceiling((decimal)_flatColors.Count / ColorsTLP.RowCount));

            float rowPercent = 100F / ColorsTLP.RowCount;
            float columnPercent = 100F / ColorsTLP.ColumnCount;

            for (int i = 0; i < ColorsTLP.RowCount; i++)
            {
                ColorsTLP.RowStyles.Add(new RowStyle(SizeType.Percent, rowPercent));
            }

            for (int i = 0; i < ColorsTLP.ColumnCount; i++)
            {
                ColorsTLP.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, columnPercent));
            }

            int flatColorIndex = 0;
            for (int row = 0; row < ColorsTLP.RowCount; row++)
            {
                for (int column = 0; column < ColorsTLP.ColumnCount; column++)
                {
                    if (flatColorIndex < _flatColors.Count)
                    {
                        FlatColorDto flatColor = _flatColors[flatColorIndex++];

                        Button colorTile = new()
                        {
                            BackColor = ColorTranslator.FromHtml(flatColor.Hex),
                            Tag = flatColor,
                            Margin = new Padding(1),
                            Anchor = (AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom)
                        };

                        colorTile.Click += new EventHandler(ColorTile_Click);

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
        #endregion
    }
}
