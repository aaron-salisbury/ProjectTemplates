using System;
using Win10App.Base.Helpers;
using Win10App.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Win10App.Views
{
    public sealed partial class FlatUIColorPickerPage : Page
    {
        public FlatUIColorPickerPage()
        {
            InitializeComponent();
            DataContext = ViewModelLocator.Current.FlatUIColorPickerViewModel;
            BuildDynamicColorGrid();
        }

        private void BuildDynamicColorGrid()
        {
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
                            Background = PlatformShim.BrushConverterConvertFrom(colorHex),
                            Command = flatUIColorPickerViewModel.ColorClickCommand,
                            CommandParameter = colorHex,
                            Margin = new Thickness(1, 1, 1, 1),
                            HorizontalAlignment = HorizontalAlignment.Stretch,
                            VerticalAlignment = VerticalAlignment.Stretch
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
