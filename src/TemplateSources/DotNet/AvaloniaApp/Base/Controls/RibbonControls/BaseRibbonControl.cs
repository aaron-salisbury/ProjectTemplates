using Avalonia.Animation;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.LogicalTree;
using Avalonia.Styling;
using AvaloniaApp.Presentation.Desktop.Base.Controls;
using RunnethOverStudio.AppToolkit.Presentation.MVVM;
using System;
using System.ComponentModel;

namespace AvaloniaApp.Base.Controls.RibbonControls;

/// <summary>
/// By ribbon content (belonging to a ViewHeaderControl) deriving from this class, 
/// its parent ViewHeaderControl will be able to display a progress ring during long running processes
/// and then a success or failure icon when the process completes.
/// </summary>
public partial class BaseRibbonControl : UserControl
{
    private readonly Animation _workflowAnimation = CreateWorkflowAnimation();
    private ViewHeaderControl? _parentViewHeaderControl;

    public BaseRibbonControl()
    {
        this.Loaded += BaseRibbonControl_Loaded;
    }

    private void BaseRibbonControl_Loaded(object? sender, RoutedEventArgs e)
    {
        _parentViewHeaderControl = this.FindLogicalAncestorOfType<ViewHeaderControl>();

        if (DataContext is BaseViewModel viewModel)
        {
            viewModel.PropertyChanged += DataContext_PropertyChanged;
        }
    }

    private void DataContext_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (sender is BaseViewModel baseViewModel)
        {
            switch (e.PropertyName)
            {
                case nameof(BaseViewModel.IsBusy):
                    if (_parentViewHeaderControl != null)
                    {
                        _parentViewHeaderControl.IsBusy = baseViewModel.IsBusy;
                    }
                    break;
                case nameof(BaseViewModel.LongRunningProcessSuccessful):
                    HandleWorkflowComplete(baseViewModel.LongRunningProcessSuccessful);
                    break;
            }
        }
    }

    private void HandleWorkflowComplete(bool? wasWorkflowSuccessful)
    {
        if (wasWorkflowSuccessful != null && _parentViewHeaderControl != null)
        {
            if (wasWorkflowSuccessful.Value)
            {
                _workflowAnimation.RunAsync(_parentViewHeaderControl.WorkflowSucessIcon);
            }
            else
            {
                _workflowAnimation.RunAsync(_parentViewHeaderControl.WorkflowFailureIcon);
            }
        }
    }

    private static Animation CreateWorkflowAnimation()
    {
        return new Animation()
        {
            Duration = TimeSpan.FromMilliseconds(2000),
            IterationCount = new IterationCount(1),
            Children =
                {
                    new KeyFrame()
                    {
                        Setters = { new Setter { Property = OpacityProperty, Value = 0.0D } },
                        Cue = new Cue(0.0D)
                    },
                    new KeyFrame()
                    {
                        Setters = { new Setter { Property = OpacityProperty, Value = 1.0D } },
                        Cue = new Cue(0.66D)
                    },
                    new KeyFrame()
                    {
                        Setters = { new Setter { Property = OpacityProperty, Value = 0.0D } },
                        Cue = new Cue(1.0D)
                    }
                }
        };
    }
}
