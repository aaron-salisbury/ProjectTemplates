using Win7App.ViewModels.SampleTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Win7App.Views.SampleTools
{
    /// <summary>
    /// Interaction logic for FlatUIColorPicker.xaml
    /// </summary>
    public partial class FlatUIColorPicker : UserControl
    {
        public FlatUIColorPicker()
        {
            InitializeComponent();
            BuildDynamicColorGrid();
        }

        private void BuildDynamicColorGrid()
        {
            // DataContext is being set in XAML.
            FlatUIColorPickerViewModel flatUIColorPickerViewModel = (FlatUIColorPickerViewModel)DataContext;

            int numberOfRows = GetNumberOfRows(flatUIColorPickerViewModel.FlatUIColorPicker.FlatColors.Count);
            int numberOfColumns = Convert.ToInt32(Math.Ceiling((decimal)flatUIColorPickerViewModel.FlatUIColorPicker.FlatColors.Count / numberOfRows));

            Grid flatColorGrid = new Grid();

            for (int i = 0; i < numberOfRows; i++)
            {
                flatColorGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
            }

            for (int i = 0; i < numberOfColumns; i++)
            {
                flatColorGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            }

            int flatColorIndex = 0;
            for (int row = 0; row < numberOfRows; row++)
            {
                for (int column = 0; column < numberOfColumns; column++)
                {
                    if (flatColorIndex < flatUIColorPickerViewModel.FlatUIColorPicker.FlatColors.Count)
                    {
                        string colorHex = flatUIColorPickerViewModel.FlatUIColorPicker.FlatColors[flatColorIndex].Hex;

                        Button colorButton = new Button
                        {
                            Background = (SolidColorBrush)(new BrushConverter().ConvertFrom(colorHex)),
                            Command = flatUIColorPickerViewModel.ColorClickCommand,
                            CommandParameter = colorHex
                        };

                        Grid.SetRow(colorButton, row);
                        Grid.SetColumn(colorButton, column);
                        flatColorGrid.Children.Add(colorButton);

                        flatColorIndex++;
                    }
                }
            }

            // "MainGrid" is set in the name attribute of the root grid in XAML.
            // We want to add this dynamic grid to the second row of the root grid so it can take up all the remaining space available.
            Grid.SetRow(flatColorGrid, 1);
            MainGrid.Children.Add(flatColorGrid);
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
