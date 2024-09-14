using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Text.RegularExpressions;

namespace System;

public partial record struct Id22
{
    public static explicit operator string(Id22 id) => id.ToString();

    /// <summary>
    /// Returns id value as string.
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return ToShortId();
    }

    /// <summary>
    /// Converts <see cref="Value"/> to Base64 22-character string.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>A Base64 string value.</returns>
    public string ToShortId() => GuidToShortId(Value);

    /// <summary>
    /// Converts <paramref name="id"/>'s Value to Base64 22-character string.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>A Base64 string value.</returns>
    public static string? ToShortId(Id22? id)
    {
        if (!id.HasValue)
            return null;

        return GuidToShortId(id.Value.Value);
    }

    /// <summary>
    /// Converts <paramref name="id"/> to Base64 22-character string.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>A Base64 string value.</returns>
    public static string GuidToShortId(Guid id)
    {
        if (Guid.Empty == id)
            return string.Empty;

        var encoded = new StringBuilder(Convert.ToBase64String(id.ToByteArray()))
            .Replace("/", "_")
            .Replace("+", "-")
            .Remove(22, 2);

        return encoded.ToString();
    }

    /// <summary>
    /// Converts <paramref name="value"/>'s Value <see cref="Id22"/> if possible.
    /// </summary>
    /// <param name="value"></param>
    /// <returns>A <see cref="Id22"/> value represented by <paramref name="value"/> short value. Or a default one if <paramref name="value"/> is not valid.</returns>
    public static Id22 FromShortId(string? value)
    {
        if (StringIsNotValidShortId(value))
            return default;

        var gid = _GuidFromShortId(value);
        return FromValue(gid);
    }

    public static Guid GuidFromShortId(string? id)
    {
        if (StringIsNotValidShortId(id))
            return Guid.Empty;

        return _GuidFromShortId(id);
    }

    /// <summary>
    /// Checks if <paramref name="value"/> is possibly not valid for conversion to <see cref="Id22"/> using <see cref="Id22.FromShortId(string?)"/>.
    /// </summary>
    /// <param name="value"></param>
    /// <returns>true if possibly not valid. Otherwise, false.</returns>
    public static bool StringIsNotValidShortId([NotNullWhen(false)] string? value) =>
        value is null || value.Length != 22 || !StringMatchesShortIdFormat(value);

    public static bool StringIsNotValidGuid([NotNullWhen(false)] string? value) =>
        value is null || value.Length != 36 || !StringMatchesGuidFormat(value);

    public static bool StringMatchesShortIdFormat(string value) =>
        Regex.IsMatch(value, "^[a-zA-Z0-9_-]{22}$");

    public static bool StringMatchesGuidFormat(string value) =>
        Regex.IsMatch(value, "^[a-fA-F0-9]{8}(-[a-fA-F0-9]{4}){3}-[a-fA-F0-9]{12}$");

    private static Guid _GuidFromShortId(string id)
    {
        id = new StringBuilder(id).Replace("_", "/").Replace("-", "+").Append("==").ToString();
        byte[] buffer = Convert.FromBase64String(id);
        return new Guid(buffer);
    }
}
