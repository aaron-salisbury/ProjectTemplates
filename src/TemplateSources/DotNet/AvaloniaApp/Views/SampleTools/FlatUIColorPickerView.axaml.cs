using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;
using DotNet.Business.Modules.Sample.DTOs;
using AvaloniaApp.Base.Extensions;
using AvaloniaApp.ViewModels;
using System;
using System.Linq;

namespace AvaloniaApp.Views;

public partial class FlatUIColorPickerView : UserControl
{
    public FlatUIColorPickerView()
    {
        InitializeComponent();
        this.SetDataContext((Avalonia.Application.Current as App)?.Services);

        InitializeColorsGrid();
    }

    private void InitializeColorsGrid()
    {
        if (DataContext is FlatUIColorPickerViewModel vm)
        {
            BrushConverter brushConverter = new();

            int colorCount = vm.FlatColors.Count;
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
                        FlatColorDto color = vm.FlatColors[colorIndex];
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

                            colorButton.Click += ColorBtn_Click;

                            Grid.SetRow(colorButton, row);
                            Grid.SetColumn(colorButton, column);
                            ColorsGrid.Children.Add(colorButton);
                        }

                        colorIndex++;
                    }
                }
            }
        }
    }

    private void ColorBtn_Click(object? sender, RoutedEventArgs e)
    {
        if (sender is Button colorBtn && DataContext is FlatUIColorPickerViewModel vm)
        {
            FlatColorDto selectedColor = vm.FlatColors.Where(color => string.Equals(color.Hex, colorBtn.Tag)).First();

            NameTB.Text = selectedColor.Name;
            HexTB.Text = selectedColor.Hex;
        }
    }

    private async void CopyBtn_Click(object? sender, RoutedEventArgs e)
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