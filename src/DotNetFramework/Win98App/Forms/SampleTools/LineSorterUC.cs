using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Win98App.Base.Extensions;
using Win98App.Base.Helpers;
using Win98Core.SampleTools;

namespace Win98App.Forms.SampleTools
{
    //TODO: This user control's design layout, among others, doesn't match the runtime's.
    public partial class LineSorterUC : UserControl
    {
        private readonly LineSorter _lineSorter;

        private List<ComboBoxEnumItem> _sortTypes;
        public List<ComboBoxEnumItem> SortTypes
        {
            get { return _sortTypes; }
            set { _sortTypes = value; }
        }

        public LineSorterUC()
        {
            InitializeComponent();

            _lineSorter = new LineSorter();

            PrepareSortTypeComboBox();
        }

        private void PrepareSortTypeComboBox()
        {
            SortTypes = new List<ComboBoxEnumItem>();
            ComboBoxEnumItem selectedComboBoxEnumItem = null;

            foreach (LineSorter.SortTypes sortType in Enum.GetValues(typeof(LineSorter.SortTypes)).Cast<LineSorter.SortTypes>())
            {
                ComboBoxEnumItem enumItem = new ComboBoxEnumItem();
                enumItem.Value = (int)sortType;
                enumItem.Text = StringExtensions.SplitPascalCase(sortType.ToString());
                SortTypes.Add(enumItem);

                if (selectedComboBoxEnumItem == null && enumItem.Value == (int)_lineSorter.SelectedSortType)
                {
                    selectedComboBoxEnumItem = enumItem;
                }
            }

            SortTypeComboBox.DataSource = SortTypes;
            SortTypeComboBox.SelectedItem = selectedComboBoxEnumItem;
            SortTypeComboBox.ValueMember = "Value"; // ComboBoxEnumItem.Value
            SortTypeComboBox.DisplayMember = "Text"; // ComboBoxEnumItem.Text
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
