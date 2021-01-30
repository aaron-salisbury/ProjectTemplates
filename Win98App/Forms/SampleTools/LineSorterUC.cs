using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Win98App.Base.Helpers;
using Win98Core.SampleTools;

namespace Win98App.Forms.SampleTools
{
    public partial class LineSorterUC : UserControl
    {
        private LineSorter _lineSorter { get; set; }

        private List<ComboBoxEnumItem> _sortTypes;
        public List<ComboBoxEnumItem> SortTypes
        {
            get => _sortTypes;
            set => _sortTypes = value;
        }

        public LineSorterUC()
        {
            InitializeComponent();

            _lineSorter = new LineSorter();

            PrepareSortTypeComboBox();
        }

        private void PrepareSortTypeComboBox()
        {
            SortTypes = Enum.GetValues(typeof(LineSorter.SortTypes))
                .Cast<LineSorter.SortTypes>()
                .Select(st => new ComboBoxEnumItem() { Value = (int)st, Text = st.ToString() })
                .ToList();

            ComboBoxEnumItem selectedComboBoxEnumItem = SortTypes
                .Where(cbi => cbi.Value == (int)_lineSorter.SelectedSortType)
                .First();

            SortTypeComboBox.DataSource = SortTypes;
            SortTypeComboBox.SelectedItem = selectedComboBoxEnumItem;
            SortTypeComboBox.ValueMember = nameof(ComboBoxEnumItem.Value);
            SortTypeComboBox.DisplayMember = nameof(ComboBoxEnumItem.Text);
        }

        private void SelectAllButton_Click(object sender, EventArgs e)
        {
            SortTextTextBox.Focus();
            SortTextTextBox.SelectAll();
        }

        private void SortButton_Click(object sender, EventArgs e)
        {
            _lineSorter.TextToSort = SortTextTextBox.Text;
            _lineSorter.Initiate();
            SortTextTextBox.Text = _lineSorter.TextToSort;
        }

        private void SortTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBoxEnumItem selectedComboBoxEnumItem = SortTypeComboBox.SelectedItem as ComboBoxEnumItem;
            _lineSorter.SelectedSortType = (LineSorter.SortTypes)Convert.ToInt32(selectedComboBoxEnumItem.Value);
        }
    }
}
