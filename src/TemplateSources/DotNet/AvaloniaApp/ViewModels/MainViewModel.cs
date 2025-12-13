using Avalonia.Controls;
using AvaloniaApp.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;
using RunnethOverStudio.AppToolkit.Presentation.MVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace AvaloniaApp.ViewModels;

public partial class MainViewModel : BaseViewModel
{
    [ObservableProperty]
    private BaseViewModel _currentPage = Ioc.Default.GetRequiredService<HomeViewModel>();

    [ObservableProperty]
    private bool _isPaneOpen;

    [ObservableProperty]
    private string _pageTitle;

    [ObservableProperty]
    private MenuPaneItemTemplate? _selectedPaneItem;

    public ObservableCollection<MenuPaneItemTemplate> PaneItems { get; }

    private readonly List<MenuPaneItemTemplate> _paneItemTemplates =
    [
        // Icon key ref: https://pictogrammers.com/library/mdi/

        new MenuPaneItemTemplate(typeof(HomeViewModel), "Home", "Home"),
        new MenuPaneItemTemplate(typeof(SampleToolsViewModel), "Tools", "Sample Tools"),
        new MenuPaneItemTemplate(typeof(LogsViewModel), "ClipboardList", "Logs")
    ];

    public MainViewModel()
    {
        IsPaneOpen = false;
        PaneItems = new ObservableCollection<MenuPaneItemTemplate>(_paneItemTemplates);
        SelectedPaneItem = PaneItems[0];
        PageTitle = SelectedPaneItem.Label;
    }

    [RelayCommand]
    private void TriggerPane()
    {
        IsPaneOpen = !IsPaneOpen;
    }

    [RelayCommand]
    private void Settings()
    {
        SelectedPaneItem = null;
        CurrentPage = Ioc.Default.GetRequiredService<SettingsViewModel>();
        PageTitle = "Settings";
    }

    partial void OnSelectedPaneItemChanged(MenuPaneItemTemplate? value)
    {
        if (value is null)
        {
            return;
        }

        object? newlySelectedViewModelObject = Design.IsDesignMode
            ? Activator.CreateInstance(value.ModelType)
            : Ioc.Default.GetService(value.ModelType);

        if (newlySelectedViewModelObject is not BaseViewModel newlySelectedViewModel)
        {
            return;
        }

        CurrentPage = newlySelectedViewModel;
        PageTitle = SelectedPaneItem?.Label ?? string.Empty;
    }
}
