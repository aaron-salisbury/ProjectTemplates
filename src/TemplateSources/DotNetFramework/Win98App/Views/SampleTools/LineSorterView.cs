using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Win98App.Base.Helpers;

namespace Win98App.Views.SampleTools;

public partial class LineSorterView : UserControl
{
    internal event EventHandler<SortCommandEventArgs> SortCommand;

    public LineSorterView()
    {
        InitializeComponent();
    }

    internal void Initialize(List<ComboBoxEnumItem> sortTypeItems)
    {
        SortTypeComboBox.DataSource = sortTypeItems;
        SortTypeComboBox.SelectedIndex = 0;
        SortTypeComboBox.ValueMember = "Value"; // ComboBoxEnumItem.Value
        SortTypeComboBox.DisplayMember = "Text"; // ComboBoxEnumItem.Text
    }

    internal void TextSorted(string text)
    {
        SortTextTextBox.Text = text;
    }

    private void SelectAllButton_Click(object sender, EventArgs e)
    {
        SortTextTextBox.Focus();
        SortTextTextBox.SelectAll();
    }

    private void SortButton_Click(object sender, EventArgs e)
    {
        if (SortCommand == null) { return; }

        SortCommand.Invoke(this, new SortCommandEventArgs()
        {
            SortTypeIndex = SortTypeComboBox.SelectedIndex,
            TextToSort = SortTextTextBox.Text
        });
    }
}

public class SortCommandEventArgs : EventArgs
{
    public int SortTypeIndex { get; set; }
    public string TextToSort { get; set; }
}
