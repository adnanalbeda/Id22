using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Microsoft.EntityFrameworkCore;

public class Id22Converters
{
    public sealed class ValueToGuidConverter()
        : ValueConverter<Id22, Guid>(v => v.Value, v => Id22.FromValue(v));

    public sealed class NullableToNullableGuidConverter()
        : ValueConverter<Id22?, Guid?>(
            v => v.HasValue ? v.Value.Value : null,
            v => v.HasValue ? Id22.FromValue(v.Value) : null
        );

    public sealed class ValueToStringConverter()
        : ValueConverter<Id22, string?>(v => v.Value.ToString(), v => Id22.Parse(v));

    public sealed class NullableToStringValueConverter()
        : ValueConverter<Id22?, string?>(
            v => v.HasValue ? v.Value.Value.ToString() : null,
            v => null == v ? null : Id22.Parse(v)
        );
}
