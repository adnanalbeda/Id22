using System.Diagnostics.CodeAnalysis;

namespace System;

/// <summary>
/// Wrapper for Guid values to convert them to and from (uri-friendly bas64 22-character string) values.
/// </summary>
/// <param name="Value"></param>
public readonly partial record struct Id22(Guid Value)
{
    public Id22()
        : this(Guid.NewGuid()) { }

    public static implicit operator Guid(Id22 id) => id.Value;

    public static implicit operator Id22(Guid id) => FromValue(id);

    /// <summary>
    /// Creates a new instance of <see cref="Id22"/> with new guid value.
    /// </summary>
    /// <returns>New <see cref="Id22"/> value.</returns>
    public static Id22 New() => new(Guid.NewGuid());

    public static Id22 FromValue(Guid id) => new(id);

    public static Id22? FromValue(Guid? id) => GuidIsEmpty(id) ? null : new(id.Value);

    public static Id22 FromValueOrDefault(Guid? id) => id.HasValue ? new(id.Value) : default;

    /// <summary>
    /// Checks if <paramref name="id"/> is valid as <see cref="Id22"/> value by testing it against <see cref="IsEmpty(Id22?)"/>.
    /// </summary>
    /// <returns><see cref="Id22"/> <paramref name="id"/> value if valid. Otherwise, a new value.</returns>
    public static Id22 ValueOrNew(Id22? id) => IsEmpty(id) ? New() : id.Value;

    /// <summary>
    /// Checks if <paramref name="id"/> is valid as <see cref="Id22"/> value by testing it against <see cref="IsEmpty(Id22?)"/>.
    /// </summary>
    /// <returns><see cref="Id22"/> <paramref name="id"/> value if valid. Otherwise, a default value.</returns>
    public static Id22 ValueOrDefault(Id22? id) => IsEmpty(id) ? default : id.Value;

    /// <summary>
    /// Checks if <paramref name="value"/> is null, default or its <see cref="Guid"/> value equals <see cref="Guid.Empty"/>
    /// </summary>
    /// <param name="value"></param>
    /// <returns>true if <paramref name="value"/> is null, default or its <see cref="Guid"/> value equals <see cref="Guid.Empty"/>. Otherwise, false.</returns>
    public static bool IsEmpty([NotNullWhen(false)] Id22? value) =>
        !value.HasValue || default == value.Value;

    public static bool GuidIsEmpty([NotNullWhen(false)] Guid? value) =>
        !value.HasValue || Guid.Empty == value.Value;
}
