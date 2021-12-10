using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using Win10App.Base.Helpers;
using Win10App.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace Win10App.Views
{
    public sealed partial class FlatUIColorPickerPage : Page
    {
        private const double SELECTED_TILE_STROKE_THICKNESS = 4D;

        public FlatUIColorPickerViewModel ViewModel => (FlatUIColorPickerViewModel)DataContext;

        public FlatUIColorPickerPage()
        {
            InitializeComponent();
            DataContext = App.Current.Services.GetService<FlatUIColorPickerViewModel>();
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

                        // Using Rectangle instead of Button because in UWP the hardcoded Button OnHover changes make this tool difficult to use.
                        Windows.UI.Xaml.Shapes.Rectangle colorTile = new Windows.UI.Xaml.Shapes.Rectangle
                        {
                            Fill = PlatformShim.BrushConverterConvertFrom(colorHex),
                            Margin = new Thickness(1, 1, 1, 1),
                            HorizontalAlignment = HorizontalAlignment.Stretch,
                            VerticalAlignment = VerticalAlignment.Stretch,
                            Tag = colorHex,
                            Stroke = Application.Current.Resources["ButtonBorderThemeBrush"] as Windows.UI.Xaml.Media.Brush,
                            StrokeThickness = string.Equals(App.Current.Services.GetService<FlatUIColorPickerViewModel>().SelectedHex, colorHex)
                                ? SELECTED_TILE_STROKE_THICKNESS
                                : 0D
                        };

                        colorTile.PointerPressed += new PointerEventHandler(ColorTile_OnClick);

                        Grid.SetRow(colorTile, row);
                        Grid.SetColumn(colorTile, column);
                        flatColorGrid.Children.Add(colorTile);

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

        private void ColorTile_OnClick(object sender, PointerRoutedEventArgs e)
        {
            foreach (Windows.UI.Xaml.Shapes.Rectangle colorRectangle in ((Grid)MainGrid.Children[1]).Children.Select(uie => uie as Windows.UI.Xaml.Shapes.Rectangle))
            {
                colorRectangle.StrokeThickness = 0D;
            }

            Windows.UI.Xaml.Shapes.Rectangle colorTile = (Windows.UI.Xaml.Shapes.Rectangle)sender;

            colorTile.StrokeThickness = SELECTED_TILE_STROKE_THICKNESS;

            // Rectangles do not have Command or Command Parameter properties, so manually firing.
            ViewModel.ColorClickCommand.Execute(colorTile.Tag);
        }
    }
}
