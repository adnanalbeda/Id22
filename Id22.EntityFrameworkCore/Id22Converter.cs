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

    // Why using Guid.ToString() instead of Id22.ToString()
    // 1- If id is generated in the db, it will be generated as full hexadecimal guid string.
    // 2- Some ids can be conflicting if stored as ShortId.
    // Like: jFIRNoMM-Ahy7FNXvdO6Ac and jFIRNoMM-Ahy7FNXvdO6AQ
    // Both are resolved to the same Guid value: 8c521136-830c-f808-72ec-5357bdd3ba01
    public sealed class ValueToStringConverter()
        : ValueConverter<Id22, string?>(v => v.Value.ToString(), v => Id22.Parse(v));

    public sealed class NullableToStringValueConverter()
        : ValueConverter<Id22?, string?>(
            v => v.HasValue ? v.Value.Value.ToString() : null,
            v => null == v ? null : Id22.Parse(v)
        );
}
