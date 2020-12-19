using System;
using System.Windows.Forms;
using WinXPCore.SampleTools;

namespace WinXPApp.Forms.SampleTools
{
    public partial class LineSorterUC : UserControl
    {
        private LineSorter _lineSorter { get; set; }

        public LineSorterUC()
        {
            InitializeComponent();

            _lineSorter = new LineSorter();

            cbSortTypes.DataSource = _lineSorter.SortTypes;
            cbSortTypes.SelectedItem = _lineSorter.SelectedSortType;
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
            _lineSorter.SelectedSortType = cbSortTypes.SelectedItem.ToString();
        }
    }
}
