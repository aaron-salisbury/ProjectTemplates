using Win7Core.Base;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Win7Core.SampleTools
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
            set { _selectedSortType = value; RaisePropertyChanged(nameof(SelectedSortType)); }
        }

        private string _textToSort;
        public string TextToSort
        {
            get => _textToSort;
            set { _textToSort = value; RaisePropertyChanged(nameof(TextToSort)); }
        }

        public LineSorter()
        {
            SelectedSortType = SortTypes.First();
        }

        public bool Initiate(ILogger logger)
        {
            try
            {
                if (!string.IsNullOrEmpty(TextToSort))
                {
                    logger.Information("Beginning to sort lines.");

                    List<string> lines = TextToSort.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None).ToList();

                    lines.Sort();

                    if (string.Equals(SelectedSortType, "Reverse Alphabetical"))
                    {
                        lines.Reverse();
                    }

                    TextToSort = new StringBuilder(string.Join("\r\n", lines.ToArray())).ToString();

                    logger.Information("Successfully sorted lines.");
                }
                else
                {
                    logger.Warning("Attempted to sort empty text.");
                }

                return true;
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
                return false;
            }
        }
    }
}
