using Avalonia.Controls;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using AvaloniaApp.Data.Domains;
using AvaloniaApp.Business.SampleTools;
using System.Linq;
using Avalonia.Interactivity;
using Avalonia.Input;

namespace AvaloniaApp.Presentation.Views
{
    public partial class FlatUIColorPickerView : UserControl
    {
        public FlatUIColorPickerView()
        {
            InitializeComponent();

            InitializeColorsGrid();
        }

        private void InitializeColorsGrid()
        {
            List<FlatColor> colors = FlatUIColorPicker.FlatColors.ToList();
            BrushConverter brushConverter = new();

            int colorCount = colors.Count;
            int numberOfRows = 4;
            int numberOfColumns = Convert.ToInt32(Math.Ceiling((decimal)colorCount / numberOfRows));

            for (int i = 0; i < numberOfRows; i++)
            {
                ColorsGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Star });
            }

            for (int i = 0; i < numberOfColumns; i++)
            {
                ColorsGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Star });
            }

            int colorIndex = 0;
            for (int row = 0; row < numberOfRows; row++)
            {
                for (int column = 0; column < numberOfColumns; column++)
                {
                    if (colorIndex < colorCount)
                    {
                        FlatColor color = colors[colorIndex];
                        object? brushObject = brushConverter.ConvertFromString(color.Hex);

                        if (brushObject != null)
                        {
                            Button colorButton = new()
                            {
                                Background = (IBrush)brushObject,
                                HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Stretch,
                                VerticalAlignment = Avalonia.Layout.VerticalAlignment.Stretch,
                                Tag = color.Hex
                            };

                            colorButton.Click += ColorBtn_OnClick;

                            Grid.SetRow(colorButton, row);
                            Grid.SetColumn(colorButton, column);
                            ColorsGrid.Children.Add(colorButton);
                        }

                        colorIndex++;
                    }
                }
            }
        }

        private void ColorBtn_OnClick(object? sender, RoutedEventArgs e)
        {
            if (sender is Button colorBtn)
            {
                FlatColor selectedColor = FlatUIColorPicker.FlatColors.Where(color => string.Equals(color.Hex, colorBtn.Tag)).First();

                NameTB.Text = selectedColor.Name;
                HexTB.Text = selectedColor.Hex;
            }
        }

        private async void CopyBtn_OnClick(object? sender, RoutedEventArgs e)
        {
            if (HexTB.Text != null)
            {
                var clipboard = TopLevel.GetTopLevel(this)?.Clipboard;

                if (clipboard != null)
                {
                    var dataObject = new DataObject();
                    dataObject.Set(DataFormats.Text, HexTB.Text);

                    await clipboard.SetDataObjectAsync(dataObject);
                }
            }
        }
    }
}
