using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Win7Core.Base;

namespace Win7Core.SampleTools
{
    public class LineSorter : ValidatableModel
    {
        private ILogger _logger { get; set; }

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

        public LineSorter(AppLogger appLogger)
        {
            _logger = appLogger.Logger;
            SelectedSortType = SortTypes.First();
        }

        public bool Initiate()
        {
            try
            {
                if (!string.IsNullOrEmpty(TextToSort))
                {
                    _logger.Information("Beginning to sort lines.");

                    List<string> lines = TextToSort.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None).ToList();

                    lines.Sort();

                    if (string.Equals(SelectedSortType, "Reverse Alphabetical"))
                    {
                        lines.Reverse();
                    }

                    TextToSort = new StringBuilder(string.Join("\r\n", lines.ToArray())).ToString();

                    _logger.Information("Successfully sorted lines.");
                }
                else
                {
                    _logger.Warning("Attempted to sort empty text.");
                }

                return true;
            }
            catch (Exception e)
            {
                _logger.Error(e.Message);
                return false;
            }
        }
    }
}
