using JetBrains.Annotations;
using Microsoft.Extensions.Logging;
using StreamingApp.Domain.Utility.Logging;

namespace StreamingApp.Domain.Utility;

public static class ArgumentValidator
{
    public static void EnsureListNotNullOrEmpty<T>(this ILogger logger, string className, ICollection<T> list, string text = "List is Null Or Empty")
    {
        if (list == null)
        {
            logger.LogMessage(LogLevel.Error, className, text);

            throw new ArgumentNullException(text);
        }

        if (!list.Any())
        {
            logger.LogMessage(LogLevel.Error, className, text);

            throw new ArgumentException(text);
        }
    }

    [AssertionMethod]
    [ContractAnnotation("value: null => halt")]
    public static T EnsureNotNull<T>([NoEnumeration][ValidatedNotNull][AssertionCondition(AssertionConditionType.IS_NOT_NULL)] T value, string name)
    {
        return Equals(value, null) ? throw new ArgumentNullException(name) : value;
    }

    public static void EnsureNotNullOrEmpty(this ILogger logger, string className, string value, string text = "Value Null or Empty")
    {
        if (string.IsNullOrEmpty(value))
        {
            logger.LogMessage(LogLevel.Error, className, text);

            throw new ArgumentNullException(text);
        }
    }

    [AssertionMethod]
    [ContractAnnotation("value: null => halt")]
    public static string EnsureNotNullOrEmpty([ValidatedNotNull][AssertionCondition(AssertionConditionType.IS_NOT_NULL)] string value, string name)
    {
        return string.IsNullOrEmpty(value) ? throw new ArgumentNullException(name) : value;
    }

    public static void EnsureStartDateBeforeEndDate(this ILogger logger, string className, DateTime startDate, DateTime endDate, string text = "StartDate cannot be after the EndDate")
    {
        if (startDate > endDate)
        {
            logger.LogMessage(LogLevel.Error, className, text);

            throw new ArgumentOutOfRangeException(text);
        }
    }

    [AttributeUsage(AttributeTargets.Parameter)]
    private sealed class ValidatedNotNullAttribute : Attribute
    { }
}
