namespace Gradeo.CoreApplication.Application.Common.Extensions;

public static class CommonExtensions
{
    public static bool IsNullOrZero(this int? number) => number is 0 or null;
    public static bool IsNullOrZero(this double? number) => number is 0 or null;
    public static bool IsNullOrZero(this decimal? number) => number is 0 or null;
    public static bool IsNullOrEmpty(this string? str) => string.IsNullOrEmpty(str);
    public static bool IsNullOrEmpty<T>(this IEnumerable<T>? list) => list == null || !list.Any();
}