using System.Diagnostics.CodeAnalysis;

namespace System;

public partial record struct Id22
{
    /// <summary>
    /// Tries to parse <paramref name="value"/> whether it's short id or regular guid value.
    /// </summary>
    /// <param name="value"></param>
    /// <returns><see cref="Id22"/> of the value if parsing is successful. Otherwise, a default value.</returns>
    public static Id22 Parse(string? s) =>
        string.IsNullOrEmpty(s)
            ? default
            : FromValue(StringMatchesShortIdFormat(s) ? _GuidFromShortId(s) : Guid.Parse(s));

    public static bool TryParse(
        [NotNullWhen(true)] string? s,
        [MaybeNullWhen(false)] out Id22 result
    )
    {
        result = default;

        if (string.IsNullOrEmpty(s))
        {
            return false;
        }

        if (StringMatchesShortIdFormat(s))
        {
            result = FromValue(_GuidFromShortId(s));
        }
        else if (Guid.TryParse(s, out Guid gid))
        {
            result = FromValue(gid);
        }

        return default != result;
    }

    public static bool StringIsParsable([NotNullWhen(true)] string? value) =>
        value is not null && (StringMatchesShortIdFormat(value) || StringMatchesGuidFormat(value));
}
