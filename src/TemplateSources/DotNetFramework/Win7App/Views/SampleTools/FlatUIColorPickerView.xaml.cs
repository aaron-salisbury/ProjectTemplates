using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Win7App.Base.Extensions;
using Win7App.ViewModels.SampleTools;

namespace Win7App.Views.SampleTools;

public partial class FlatUIColorPickerView : UserControl
{
    public FlatUIColorPickerView()
    {
        InitializeComponent();
        this.SetDataContext((System.Windows.Application.Current as App)?.Services);
        BuildDynamicColorGrid();
    }

    private void BuildDynamicColorGrid()
    {
        if (DataContext is FlatUIColorPickerViewModel viewModel)
        {
            int numberOfRows = GetNumberOfRows(viewModel.FlatColors.Count);
            int numberOfColumns = Convert.ToInt32(Math.Ceiling((decimal)viewModel.FlatColors.Count / numberOfRows));

            Grid flatColorGrid = new();

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
                    if (flatColorIndex < viewModel.FlatColors.Count)
                    {
                        string colorHex = viewModel.FlatColors[flatColorIndex].Hex;

                        Button colorButton = new Button
                        {
                            Background = (SolidColorBrush)(new BrushConverter().ConvertFrom(colorHex)),
                            Command = viewModel.ColorClickCommand,
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
