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

        public override object? ConvertFrom(
            ITypeDescriptorContext? context,
            CultureInfo? culture,
            object value
        ) =>
            value is Guid gid ? Id22.FromValue(gid)
            : value is string uuid ? Id22.Parse(uuid)
            : base.ConvertFrom(context, culture, value);

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
