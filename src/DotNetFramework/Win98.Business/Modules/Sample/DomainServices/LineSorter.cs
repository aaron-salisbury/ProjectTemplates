using DotNetFramework.Core.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Win98.Business.Modules.Sample.DomainServices
{
    public class LineSorter
    {
        private static readonly string[] SEPARATOR = ["\r\n", "\r", "\n"];

        public enum SortTypes
        {
            Alphabetical,
            ReverseAlphabetical
        }

        private readonly ILogger _logger;

        public LineSorter(ILogger logger)
        {
            _logger = logger;
        }

        public string Initiate(string textToSort, SortTypes selectedSortType = SortTypes.Alphabetical)
        {
            string sortedText;

            try
            {
                if (!string.IsNullOrEmpty(textToSort))
                {
                    List<string> lines = [.. textToSort.Split(SEPARATOR, StringSplitOptions.None)];

                    lines.Sort();

                    if (selectedSortType == SortTypes.ReverseAlphabetical)
                    {
                        lines.Reverse();
                    }

                    sortedText = new StringBuilder(string.Join("\r\n", [.. lines])).ToString();
                }
                else
                {
                    _logger.LogWarning("Attempted to sort empty text.");
                    sortedText = string.Empty;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to sort text.");
                sortedText = null;
            }

            return sortedText;
        }
    }
}
