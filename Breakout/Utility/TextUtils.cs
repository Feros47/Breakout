namespace Breakout.Utility;

/// <summary>
/// Utility class providing extension methods for string operations used in the level loader.
/// </summary>
public static class TextUtils {
    /// <summary>
    /// Split a string using a separator and trim the individual elements.
    /// </summary>
    /// <param name="input">The string to split</param>
    /// <param name="separator">Separator character</param>
    /// <returns>A <see cref="List{string}"/> containing each element.</returns>
    public static List<string> SplitAndTrim(this string input, char separator) {
        var splitted = input.Split(separator);
        return splitted
            .Select(s => s.Trim())
            .ToList();
    }

    /// <summary>
    /// Split a metadata entry on the first separator character (colon usually) and trim the elements.
    /// </summary>
    /// <param name="input">The input string</param>
    /// <param name="separator">Separator character</param>
    /// <returns>A pair representing the key and value respectively</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if no separator occurs</exception>
    public static (string, string) SplitOnFirstAndTrim(this string input, char separator) {
        var index = input.IndexOf(separator);
        if (index == -1) {
            throw new ArgumentOutOfRangeException($"Invalid input: \"{input}\" does not contain separator '{separator}'");
        }
        var key = input
            .Substring(0, index)
            .Trim();
        var value = input
            .Substring(index + 1)
            .Trim();
        return (key, value);
    }
}
