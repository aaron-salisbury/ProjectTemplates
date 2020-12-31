using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Win7App.Base.Extensions;
using WinXPApp.Base.Helpers;
using WinXPCore.Base.DataAnnotationValidation;
using WinXPCore.SampleTools;

namespace WinXPApp.Forms.SampleTools
{
    public partial class LineSorterUC : UserControl
    {
        private LineSorter _lineSorter { get; set; }

        private List<ComboBoxEnumItem> _sortTypes;
        public List<ComboBoxEnumItem> SortTypes
        {
            get => _sortTypes;
            set =>_sortTypes = value;
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
                .Select(st => new ComboBoxEnumItem() { Value = (int)st, Text = st.GetAttribute<DisplayAttribute>()?.Name ?? st.ToString() })
                .ToList();

            ComboBoxEnumItem selectedComboBoxEnumItem = SortTypes
                .Where(cbi => cbi.Value == (int)_lineSorter.SelectedSortType)
                .First();

            cbSortTypes.DataSource = SortTypes;
            cbSortTypes.SelectedItem = selectedComboBoxEnumItem;
            cbSortTypes.ValueMember = nameof(ComboBoxEnumItem.Value);
            cbSortTypes.DisplayMember = nameof(ComboBoxEnumItem.Text);
        }

        private void BtnSelectAll_Click(object sender, EventArgs e)
        {
            tbTextToSort.Focus();
            tbTextToSort.SelectAll();
        }

        private void BtnSort_Click(object sender, EventArgs e)
        {
            _lineSorter.TextToSort = tbTextToSort.Text;
            _lineSorter.Initiate();
            tbTextToSort.Text = _lineSorter.TextToSort;
        }

        private void CbSortTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBoxEnumItem selectedComboBoxEnumItem = cbSortTypes.SelectedItem as ComboBoxEnumItem;
            _lineSorter.SelectedSortType = (LineSorter.SortTypes)Convert.ToInt32(selectedComboBoxEnumItem.Value);
        }
    }
}
