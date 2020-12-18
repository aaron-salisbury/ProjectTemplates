using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using WinXPCore.Base;

namespace WinXPCore.SampleTools
{
    public class LineSorter : ValidatableModel
    {
        public List<string> SortTypes
        {
            get => new List<string>() { "Alphabetical", "Reverse Alphabetical" };
        }

        private string _selectedSortType;
        [Required]
        public string SelectedSortType
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
            SelectedSortType = SortTypes.First();
        }

        public bool Initiate()
        {
            try
            {
                if (!string.IsNullOrEmpty(TextToSort))
                {
                    Logger.Info("Beginning to sort lines.");

                    List<string> lines = TextToSort.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None).ToList();

                    lines.Sort();

                    if (string.Equals(SelectedSortType, "Reverse Alphabetical"))
                    {
                        lines.Reverse();
                    }

                    TextToSort = new StringBuilder(string.Join("\r\n", lines.ToArray())).ToString();

                    Logger.Info("Successfully sorted lines.");
                }
                else
                {
                    Logger.Warn("Attempted to sort empty text.");
                }

                return true;
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
                return false;
            }
        }
    }
}
