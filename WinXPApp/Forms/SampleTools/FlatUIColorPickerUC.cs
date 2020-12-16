using MetroFramework.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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
            //TODO: Panel isn't changing size with its parent form.

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

            float rowRercent = 100 / tlpColors.RowCount;
            float columnRercent = 100 / tlpColors.ColumnCount;

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
                        string colorHex = _flatUIColorPicker.FlatColors[flatColorIndex].Hex;

                        MetroTile colorTile = new MetroTile()
                        {
                            UseCustomBackColor = true,
                            BackColor = ColorTranslator.FromHtml(colorHex),
                            Margin = new Padding(1),
                            Anchor = (AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom)
                            //Command = flatUIColorPickerViewModel.ColorClickCommand,
                            //CommandParameter = colorHex
                        };

                        tlpColors.Controls.Add(colorTile);
                        tlpColors.SetRow(colorTile, row);
                        tlpColors.SetColumn(colorTile, column);

                        flatColorIndex++;
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
    }
}
