using DotNet.Business.Modules.Sample.Events;
using Microsoft.Extensions.Logging;
using RunnethOverStudio.AppToolkit.Modules.Messaging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;

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

        private readonly IEventSystem _events;
        private readonly ILogger _logger;

        public LineSorter(IEventSystem eventSystem, ILogger logger)
        {
            _events = eventSystem;
            _logger = logger;
        }

        public async Task InitiateAsync(string? textToSort, SortTypes selectedSortType = SortTypes.Alphabetical)
        {
            await Task.Run(() =>
            {
                string? sortedText;

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

                _events.Publish(this, new TextSorted() { SortedText = sortedText });
            });
        }
    }
}
