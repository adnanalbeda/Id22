using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Microsoft.EntityFrameworkCore;

public static partial class Id22ConverterExtensions
{
    private static Action<PropertiesConfigurationBuilder<Id22>> defaultValueDelegate = (_) => { };
    private static Action<PropertiesConfigurationBuilder<Id22?>> defaultNullableValueDelegate = (
        _
    ) => { };

    /// <summary>
    ///Calls both <see cref="MapId22ValueToGuid"/> and <see cref="MapId22NullableToNullableGuid"/>.
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="extendValueProperty"></param>
    /// <param name="extendNullableValueProperty"></param>
    /// <returns></returns>
    public static ModelConfigurationBuilder MapId22ToGuid(
        this ModelConfigurationBuilder builder,
        Action<PropertiesConfigurationBuilder<Id22>>? extendValueProperty = null,
        Action<PropertiesConfigurationBuilder<Id22?>>? extendNullableValueProperty = null
    ) =>
        builder
            .MapId22ValueToGuid(extendValueProperty)
            .MapId22NullableToNullableGuid(extendNullableValueProperty);

    public static ModelConfigurationBuilder MapId22ValueToGuid(
        this ModelConfigurationBuilder builder,
        Action<PropertiesConfigurationBuilder<Id22>>? extendValueProperty = null
    )
    {
        extendValueProperty ??= defaultValueDelegate;

        extendValueProperty(
            builder.Properties<Id22>().HaveConversion<Id22Converters.ValueToGuidConverter>()
        );

        return builder;
    }

    public static ModelConfigurationBuilder MapId22NullableToNullableGuid(
        this ModelConfigurationBuilder builder,
        Action<PropertiesConfigurationBuilder<Id22?>>? extendNullableValueProperty = null
    )
    {
        extendNullableValueProperty ??= defaultNullableValueDelegate;

        extendNullableValueProperty(
            builder
                .Properties<Id22?>()
                .HaveConversion<Id22Converters.NullableToNullableGuidConverter>()
        );

        return builder;
    }

    public static ModelConfigurationBuilder MapId22ToString(
        this ModelConfigurationBuilder builder,
        Action<PropertiesConfigurationBuilder<Id22>>? extendValueProperty = null,
        Action<PropertiesConfigurationBuilder<Id22?>>? extendNullableValueProperty = null
    ) =>
        builder
            .MapId22ValueToString(extendValueProperty)
            .MapId22NullableToString(extendNullableValueProperty);

    public static ModelConfigurationBuilder MapId22ValueToString(
        this ModelConfigurationBuilder builder,
        Action<PropertiesConfigurationBuilder<Id22>>? extendValueProperty = null
    )
    {
        extendValueProperty ??= defaultValueDelegate;

        extendValueProperty(
            builder.Properties<Id22>().HaveConversion<Id22Converters.ValueToStringConverter>()
        );

        return builder;
    }

    public static ModelConfigurationBuilder MapId22NullableToString(
        this ModelConfigurationBuilder builder,
        Action<PropertiesConfigurationBuilder<Id22?>>? extendNullableValueProperty = null
    )
    {
        extendNullableValueProperty ??= defaultNullableValueDelegate;

        extendNullableValueProperty(
            builder
                .Properties<Id22?>()
                .HaveConversion<Id22Converters.NullableToStringValueConverter>()
        );

        return builder;
    }
}
