namespace System;

// These interfaces are necessary for EF & converters to work as expected.
public partial record struct Id22 : IEquatable<Id22?>, IEquatable<Id22>, IEquatable<Guid>
{
    public bool Equals(Id22? other)
    {
        return other.HasValue && Equals(other.Value.Value);
    }

    public bool Equals(Guid other)
    {
        return Value.Equals(other);
    }
}
