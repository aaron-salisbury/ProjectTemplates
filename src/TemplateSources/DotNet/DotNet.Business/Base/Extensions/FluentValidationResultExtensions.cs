using FluentValidation.Results;
using System;
using System.Linq;

namespace DotNet.Business.Base.Extensions;

public static class FluentValidationResultExtensions
{
    /// <summary>
    /// Generates a string representation of the error messages separated by the '*' character and, when there is more than one, on separate new lines.
    /// All after a "{processNameThatFailed} failed: " prefix.
    /// </summary>
    /// <param name="processNameThatFailed">The name of what failed; to prefix the error messages with.</param>
    public static string ToStringPretty(this ValidationResult result, string processNameThatFailed)
    {
        if (result.Errors.Count == 0)
        {
            return string.Empty;
        }

        string errorMessage = result.Errors.Count == 1
            ? result.Errors.First().ErrorMessage
            : Environment.NewLine + string.Join(Environment.NewLine, result.Errors.Select(e => $" * {e.ErrorMessage}"));

        return $"{processNameThatFailed} failed: {errorMessage}";
    }
}
