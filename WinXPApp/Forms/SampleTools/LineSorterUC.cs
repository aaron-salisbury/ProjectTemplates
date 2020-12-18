using System;
using System.Windows.Forms;
using WinXPCore.SampleTools;

namespace WinXPApp.Forms.SampleTools
{
    public partial class LineSorterUC : UserControl
    {
        public LineSorter LineSorter { get; set; }

        public LineSorterUC()
        {
            InitializeComponent();

            LineSorter = new LineSorter();

            cbSortTypes.DataSource = LineSorter.SortTypes;
            cbSortTypes.SelectedItem = LineSorter.SelectedSortType;
        }

        private void BtnSelectAll_Click(object sender, EventArgs e)
        {
            tbTextToSort.Focus();
            tbTextToSort.SelectAll();
        }

        private void BtnSort_Click(object sender, EventArgs e)
        {
            LineSorter.TextToSort = tbTextToSort.Text;
            LineSorter.Initiate();
            tbTextToSort.Text = LineSorter.TextToSort;
        }

        private void CbSortTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            LineSorter.SelectedSortType = cbSortTypes.SelectedItem.ToString();
        }
    }
}
