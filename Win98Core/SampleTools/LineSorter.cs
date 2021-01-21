using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Win98Core.Base;

namespace Win98Core.SampleTools
{
    public class LineSorter
    {
        public enum SortTypes
        {
            Alphabetical,
            ReverseAlphabetical
        }

        private SortTypes _selectedSortType;
        public SortTypes SelectedSortType
        {
            get => _selectedSortType;
            set => _selectedSortType = value;
        }

        private string _textToSort;
        public string TextToSort
        {
            get => _textToSort;
            set => _textToSort = value;
        }

        public LineSorter()
        {
            SelectedSortType = SortTypes.Alphabetical;
        }

        public bool Initiate()
        {
            try
            {
                if (!string.IsNullOrEmpty(TextToSort))
                {
                    AppLogger.Write("Beginning to sort lines.", AppLogger.LogCategories.Information);

                    List<string> lines = TextToSort.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None).ToList();

                    lines.Sort();

                    if (SelectedSortType == SortTypes.ReverseAlphabetical)
                    {
                        lines.Reverse();
                    }

                    TextToSort = new StringBuilder(string.Join("\r\n", lines.ToArray())).ToString();

                    AppLogger.Write("Successfully sorted lines.", AppLogger.LogCategories.Information);
                }
                else
                {
                    AppLogger.Write("Attempted to sort empty text.", AppLogger.LogCategories.Warning);
                }

                return true;
            }
            catch (Exception e)
            {
                AppLogger.Write(e.Message, AppLogger.LogCategories.Error);
                return false;
            }
        }
    }
}
