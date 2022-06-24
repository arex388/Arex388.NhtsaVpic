namespace System.Collections.Generic;

internal static class EnumerableExtensions {
    public static string StringJoin<T>(
        this IEnumerable<T> enumerable,
        string separator) => string.Join(separator, enumerable);
}