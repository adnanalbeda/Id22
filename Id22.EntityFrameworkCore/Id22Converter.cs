using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Microsoft.EntityFrameworkCore;

public class Id22Converters
{
    public sealed class ValueToGuidConverter()
        : ValueConverter<Id22, Guid>(v => v.Value, v => Id22.FromValue(v));

    public sealed class NullableToNullableGuidConverter()
        : ValueConverter<Id22?, Guid?>(
            v => v.HasValue ? v.Value.Value : null,
            v => Id22.GuidIsEmpty(v) ? null : Id22.FromValue(v.Value)
        );

    public sealed class ValueToStringConverter()
        : ValueConverter<Id22, string?>(
            v => Guid.Empty == v.Value ? null : v.Value.ToString(),
            v => Id22.Parse(v)
        );

    public sealed class NullableToStringValueConverter()
        : ValueConverter<Id22?, string?>(
            v => Id22.IsEmpty(v) ? null : v.Value.ToString(),
            v => string.IsNullOrWhiteSpace(v) ? null : Id22.Parse(v)
        );
}
