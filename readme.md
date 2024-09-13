# Id22

A wrapper against Guid.

<!-- TOC -->

- [Id22](#id22)
    - [Purpose](#purpose)
    - [Usage](#usage)
    - [Utils](#utils)
    - [Converters](#converters)
        - [System.Id22Converters](#systemid22converters)
        - [Microsoft.EntityFramework.Id22Converters](#microsoftentityframeworkid22converters)
            - [Setup](#setup)
    - [Tips](#tips)
        - [Swagger](#swagger)

<!-- /TOC -->


## Purpose

I made this library to solve the issue of serializing Guid to string for web apis.

It makes them url friendly with the usage of `Base64` and short (22 characters).

While it's possible to use libraries for the purpose of converting to such,
no library (that I know of) has made a type that can be stored as Guid, yet presented as string.

So this library provides, with the help of converters, `Id22` can be used instead of `Guid`, to be stored as Guid, but viewed as a nice short string.


## Usage

To use in you code, you just need this:

```cs
public class Book
{
    // No need to add or prepare anything, except for ef DbContext.
    public Id22 Id { get; set; } 
    
    public required string Name { get; set; }

    // Maybe this attribute is not necessary. 
    // Testing is required.
    [JsonConverter(typeof(Id22Converters.NullableJsonConverter))]
    public Id22? PublisherId { get; set; }
}
```

## Utils

Id22 provides:

- `New` to create new value.
- `ValueOrNew` checks if value is valid for use. Returns new one if not.
- `ValueOrDefault` checks if value is valid for use. Returns default if not.
- `IsEmpty` to check if value is null, default or empty guid.

String Utils:

- `ToShortId` to convert `Id22` to base64 22-character string.
- `FromShortId` to convert string to `Id22` if possible.
- `StringIsNotValidShortId` to check whether it's possible to convert string back to `Id22` or not.
- `Parse` to convert string to Id22 whether it's guid format or short id format.

## Converters

Converters are provided by `System.Id22Converters` and `Microsoft.EntityFramework.Id22Converters` classes.

> [!Warning]
>
> All converters are using `ToString()` when converting to string value so implementation is unified across them. 
> This means if you use string converter for `DbContext`, it will store short ids.
> This may cause issues, so better not to use it, but use guid converter instead. 

### System.Id22Converters

This class provides implementations for:

- TypeConverter: applied by `[TypeConverter]` annotation. No need for extra setup.
- JsonConverter: also applied by `[JsonConverter]` annotation. Might need to apply nullable converter.

### Microsoft.EntityFramework.Id22Converters

This class provides implementations of `ValueConverter` for EF Core.

It's available through nuget package `Id22.EntityFrameworkCore`.

#### Setup

Usage with `DbContext`:

```cs
public class DataContext : DbContext
{
    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);

        // ... configs

        configurationBuilder.MapId22ToGuid(); 

        // or to store as string

        // If string values can be generated as guid from the db,
        // then avoid using this.
        configurationBuilder.MapId22ToString();

        // ... configs
    }
}
```
## Tips

If you have a global using file, you can add:

```cs
global using Id = Id22;
```

so you use `Id` instead of `Id22`.

### Swagger

When using swagger, it can't pickup the json serializer.

So add this config line to show al Id22 properties as string:

```cs
builder.Services.AddSwaggerGen(
    (opt) =>
    {

        ...configs

        opt.MapType<Id>(
            () =>
                new OpenApiSchema
                {
                    Type = "string",
                    Example = new OpenApiString("A1C2E3G4I5K6M7O8Q9E0-_"),
                }
        );

        ...configs
    }
)
```
