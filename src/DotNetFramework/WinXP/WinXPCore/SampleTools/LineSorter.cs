using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using WinXPCore.Base;
using WinXPCore.Base.DataAnnotationValidation;

namespace WinXPCore.SampleTools
{
    public class LineSorter : ValidatableModel
    {
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
            get { return _selectedSortType; }
            set { _selectedSortType = value; }
        }

        private string _textToSort;
        public string TextToSort
        {
            get { return _textToSort; }
            set { _textToSort = value; }
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
                    Logger.Info("Beginning to sort lines.");

                    List<string> lines = TextToSort.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None).ToList();

                    lines.Sort();

                    if (SelectedSortType == SortTypes.ReverseAlphabetical)
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
