using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using WinXPApp.Base.Extensions;
using WinXPApp.Base.Helpers;
using WinXPCore.Base.DataAnnotationValidation;
using WinXPCore.SampleTools;

namespace WinXPApp.Forms.SampleTools
{
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
                DisplayAttribute enumDisplayAttribute = sortType.GetAttribute<DisplayAttribute>();

                ComboBoxEnumItem enumItem = new ComboBoxEnumItem
                {
                    Value = (int)sortType,
                    Text = enumDisplayAttribute != null ? enumDisplayAttribute.Name : sortType.ToString()
                };

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

        private void BtnSelectAll_Click(object sender, EventArgs e)
        {
            SortTextTextBox.Focus();
            SortTextTextBox.SelectAll();
        }

        private void BtnSort_Click(object sender, EventArgs e)
        {
            _lineSorter.TextToSort = SortTextTextBox.Text;
            _lineSorter.Initiate();
            SortTextTextBox.Text = _lineSorter.TextToSort;
        }

        private void CbSortTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBoxEnumItem selectedComboBoxEnumItem = SortTypeComboBox.SelectedItem as ComboBoxEnumItem;
            _lineSorter.SelectedSortType = (LineSorter.SortTypes)Convert.ToInt32(selectedComboBoxEnumItem.Value);
        }
    }
}
