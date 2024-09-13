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
        s is null
            ? default
            : FromValue(StringIsNotValidShortId(s) ? Guid.Parse(s) : _GuidFromShortId(s));

    public static bool TryParse(
        [NotNullWhen(true)] string? s,
        [MaybeNullWhen(false)] out Id22 result
    )
    {
        result = default;

        if (s is null)
        {
            return false;
        }

        if (StringIsNotValidShortId(s))
            if (Guid.TryParse(s, out Guid gid))
            {
                result = FromValue(gid);
                return true;
            }
            else
                return false;

        result = FromValue(_GuidFromShortId(s));

        return default != result;
    }
}
