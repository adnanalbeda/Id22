using System.Diagnostics.CodeAnalysis;

namespace System;

public partial record struct Id22
{
    /// <summary>
    /// Tries to parse <paramref name="value"/> whether it's short id or regular guid value.
    /// </summary>
    /// <param name="value"></param>
    /// <returns><see cref="Id22"/> of the value if parsing is successful.</returns>
    /// <exception cref="FormatException"/>
    public static Id22 Parse(string? s)
    {
        if (string.IsNullOrEmpty(s))
            return default;

        return FromValue(StringMatchesShortIdFormat(s) ? _GuidFromShortId(s) : Guid.Parse(s));
    }

    /// <summary>
    /// Tries to parse
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static Id22 SafeParse(string? s)
    {
        if (string.IsNullOrWhiteSpace(s))
            return default;

        if (StringMatchesShortIdFormat(s))
            return FromValue(_GuidFromShortId(s));

        return Guid.TryParse(s, out Guid gid) ? FromValue(gid) : default;
    }

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
