using AvaloniaApp.Business.Base.Extensions;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Text;

namespace AvaloniaApp.Business.SampleTools
{
    public static class LineSorter
    {
        public enum SortTypes
        {
            Alphabetical,
            [Display(Name = "Reverse Alphabetical")]
            ReverseAlphabetical
        }

        public static string? Initiate(SortTypes _selectedSortType, string? textToSort)
        {
            string? sortedText = null;

            try
            {
                if (!string.IsNullOrEmpty(textToSort))
                {
                    Debug.WriteLine("Beginning to sort lines.", "INFO");

                    List<string> lines = textToSort.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None).ToList();

                    lines.Sort();

                    if (_selectedSortType == SortTypes.ReverseAlphabetical)
                    {
                        lines.Reverse();
                    }

                    sortedText = new StringBuilder(string.Join("\r\n", lines.ToArray())).ToString();

                    Debug.WriteLine("Successfully sorted lines.", "INFO");
                }
                else
                {
                    Debug.WriteLine("Attempted to sort empty text.", "WARN");
                    sortedText = string.Empty;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message, "ERROR");
            }

            return sortedText;
        }

        public static string GetSortTypeDisplayName(SortTypes sortType)
        {
            DisplayAttribute? displayAttribute = sortType.GetAttribute<DisplayAttribute>();

            if (displayAttribute != null && displayAttribute.Name != null)
            {
                return displayAttribute.Name;
            }

            return sortType.ToString();
        }
    }
}
