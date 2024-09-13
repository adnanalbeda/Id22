using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Security.Principal;

namespace System;

[TypeConverter(typeof(Id22Converters.TypeConverter))]
public partial record struct Id22;

public partial class Id22Converters
{
    public sealed class TypeConverter : ComponentModel.TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType) =>
            sourceType == typeof(Guid)
            || sourceType == typeof(Guid?)
            || sourceType == typeof(string)
            || base.CanConvertFrom(context, sourceType);

        // Use of Id22.Parse is intended so it only accepts:
        // - null or empty string.
        // - string that matches short id or guid id formats.
        // If any other string value type is passed, error will be thrown.
        // This helps to reduce checks.
        // In asp, for example, it helps to decline requests with invalid ids before reaching controller,
        // so only valid ids will be passed for processing and no need for extra checks.
        // At the same time, it avoids invalid string values to be processed as default,
        // so no weird unexpected happy processing happens.
        public override object? ConvertFrom(
            ITypeDescriptorContext? context,
            CultureInfo? culture,
            object value
        )
        {
            if (value is null)
                return default(Id22);

            if (value is Guid gid)
                return Id22.FromValue(gid);

            if (value is string id)
                return Id22.Parse(id);

            return base.ConvertFrom(context, culture, value);
        }

        public override bool CanConvertTo(
            ITypeDescriptorContext? context,
            [NotNullWhen(true)] Type? destinationType
        ) =>
            destinationType == typeof(Guid)
            || destinationType == typeof(Guid?)
            || destinationType == typeof(string)
            || base.CanConvertTo(context, destinationType);

        public override object? ConvertTo(
            ITypeDescriptorContext? context,
            CultureInfo? culture,
            object? value,
            Type destinationType
        )
        {
            if (value is not Id22 uid)
            {
                return base.ConvertTo(context, culture, value, destinationType);
            }

            if (destinationType == typeof(Guid) || destinationType == typeof(Guid?))
            {
                return uid.Value;
            }

            if (destinationType == typeof(string))
            {
                uid.ToString();
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
