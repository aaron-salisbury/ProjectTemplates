using DotNet.Business.Modules.Sample.MessageContracts;
using MassTransit;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DotNet.Business.Modules.Sample.DomainServices
{
    public class LineSorter
    {
        private static readonly string[] SEPARATOR = ["\r\n", "\r", "\n"];

        public enum SortTypes
        {
            Alphabetical,
            [Display(Name = "Reverse Alphabetical")]
            ReverseAlphabetical
        }

        private readonly IBus _bus;
        private readonly ILogger _logger;

        public LineSorter(IBus bus, ILogger logger)
        {
            _bus = bus;
            _logger = logger;
        }

        public async Task InitiateAsync(SortTypes _selectedSortType, string? textToSort)
        {
            string? sortedText;

            try
            {
                if (!string.IsNullOrEmpty(textToSort))
                {
                    List<string> lines = [.. textToSort.Split(SEPARATOR, StringSplitOptions.None)];

                    lines.Sort();

                    if (_selectedSortType == SortTypes.ReverseAlphabetical)
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

            await _bus.Publish<TextSorted>(new { SortedText = sortedText });
        }
    }
}
