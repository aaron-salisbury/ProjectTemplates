using DotNetFrameworkToolkit.Modules.ComponentModel;
using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using Win7App.Base.Services;

namespace Win7App.Base;

public partial class BaseViewModel : ObservableValidator, System.ComponentModel.INotifyDataErrorInfo
{
    public event EventHandler<System.ComponentModel.DataErrorsChangedEventArgs> ErrorsChanged;

    private bool _isDirty;
    public bool IsDirty
    {
        get { return _isDirty; }
        set { SetField(ref _isDirty, value, nameof(IsDirty)); }
    }

    private bool _isBusy;
    public bool IsBusy
    {
        get { return _isBusy; }
        set
        {
            SetField(ref _isBusy, value, nameof(IsBusy));
            ProgressBarVisibility = _isBusy ? Visibility.Visible : Visibility.Hidden;
        }
    }

    private bool? _longRunningProcessSuccessful;
    public bool? LongRunningProcessSuccessful
    {
        get { return _longRunningProcessSuccessful; }
        set { SetField(ref _longRunningProcessSuccessful, value, nameof(LongRunningProcessSuccessful)); }
    }

    protected override void OnErrorsChanged(object sender, string propertyName)
    {
        ErrorsChanged?.Invoke(sender, new System.ComponentModel.DataErrorsChangedEventArgs(propertyName));

        base.OnErrorsChanged(sender, propertyName);
    }

    private Visibility _progressBarVisibility = Visibility.Hidden;
    public Visibility ProgressBarVisibility
    {
        get { return _progressBarVisibility; }
        set { SetField(ref _progressBarVisibility, value, nameof(ProgressBarVisibility)); }
    }

    public virtual void AddModelEvents() { }

    public virtual void RemoveModelEvents() { }

    /// <summary>
    /// Indicate that a process completed successfully.
    /// </summary>
    public static void ShowSuccessDialog()
    {
        ModernDialog modernDialog = new()
        {
            Content = new Views.SuccessModal()
        };
        modernDialog.Buttons = [modernDialog.OkButton];
        modernDialog.ShowDialog();
    }

    /// <summary>
    /// Execute long running process without locking the UI thread.
    /// </summary>
    /// <param name="longRunningFunction">Function to execute. If its process fails, it needs to return false or null.</param>
    internal async Task<T> InitiateLongRunningProcessAsync<T>(Func<T> longRunningFunction, IAgnosticDispatcher dispatcher)
    {
        try
        {
            IsBusy = true;

            return await dispatcher.InvokeOnBackgroundAsync(() =>
            {
                TaskCompletionSource<T> tcs = new();

                Task.Run(() =>
                {
                    T result = longRunningFunction.Invoke();
                    tcs.SetResult(result);
                }).ConfigureAwait(false);

                LongRunningProcessSuccessful = (tcs.Task.Result is bool boolResult) ? boolResult : tcs.Task.Result != null;

                return tcs.Task;
            });
        }
        finally
        {
            IsBusy = false;
            LongRunningProcessSuccessful = null;
        }
    }

    protected void ObservableProperty_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (!string.Equals(e.PropertyName, nameof(IsDirty)))
        {
            IsDirty = true;
        }
    }
}
