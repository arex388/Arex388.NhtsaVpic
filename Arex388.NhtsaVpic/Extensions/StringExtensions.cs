namespace System.Collections.Generic;

internal static class StringExtensions {
    public static bool HasValue(
        this string value) => !string.IsNullOrEmpty(value);
}