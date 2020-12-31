using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Win7Core.Base;
using Win7Core.Base.Extensions;

namespace Win7Core.SampleTools
{
    public class LineSorter : ValidatableModel
    {
        private ILogger _logger { get; set; }

        public enum SortTypes
        {
            Alphabetical,
            [Display(Name = "Reverse Alphabetical")]
            ReverseAlphabetical
        }

        private SortTypes _selectedSortType;
        [Required]
        public SortTypes SelectedSortType
        {
            get => _selectedSortType;
            set
            {
                _selectedSortType = value;
                RaisePropertyChanged(nameof(SelectedSortType));
            }
        }

        public static string GetSortTypeDisplayName(SortTypes sortType) => sortType.GetAttribute<DisplayAttribute>()?.Name ?? sortType.ToString();

        private string _textToSort;
        public string TextToSort
        {
            get => _textToSort;
            set { _textToSort = value; RaisePropertyChanged(nameof(TextToSort)); }
        }

        public LineSorter(AppLogger appLogger)
        {
            _logger = appLogger.Logger;
            SelectedSortType = SortTypes.Alphabetical;
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

                    if (SelectedSortType == SortTypes.ReverseAlphabetical)
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
