# graphql-dotnetcore

Add [EntityFramework Core IQueryable](https://docs.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.dbset-1.system-linq-iqueryable-provider) support to [GraphQL](https://github.com/graphql-up/graphql-dotnetcore)

## Query Usage


### Arguments

The arguments supported are `ids`, `where`, `orderBy` , `skip`, and `take`.

Arguments are executed in that order.


#### Ids

Queries entities by id. Currently the only supported identity member (property or field) name is `Id`.


##### Supported Types

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string), [Guid](https://docs.microsoft.com/en-us/dotnet/api/system.guid), [Double](https://docs.microsoft.com/en-us/dotnet/api/system.double), [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean), [Float](https://docs.microsoft.com/en-us/dotnet/api/system.float), [Byte](https://docs.microsoft.com/en-us/dotnet/api/system.byte), [DateTime](https://docs.microsoft.com/en-us/dotnet/api/system.datetime), [DateTimeOffset](https://docs.microsoft.com/en-us/dotnet/api/system.datetimeoffset), [Decimal](https://docs.microsoft.com/en-us/dotnet/api/system.decimal), [Int16](https://docs.microsoft.com/en-us/dotnet/api/system.int16), [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32), [Int64](https://docs.microsoft.com/en-us/dotnet/api/system.int64), [UInt16](https://docs.microsoft.com/en-us/dotnet/api/system.uint16), [UInt32](https://docs.microsoft.com/en-us/dotnet/api/system.uint32), and [UInt64](https://docs.microsoft.com/en-us/dotnet/api/system.uint64).


##### Single

```graphql
{
  entities (ids: "1")
  {
    property
  }
}
```


##### Multiple

```graphql
{
  entities (ids: ["1", "2"])
  {
    property
  }
}
```


#### Where

Where statements are [and'ed](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/conditional-and-operator) together and executed in order.


##### Property Path

All where statements require a `path`. This is a full path to a, possible nested, property. Eg a property at the root level could be `Address`, while a nested property could be `Address.Street`. No null checking of nested values is done.


##### Supported Types

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string), [Guid](https://docs.microsoft.com/en-us/dotnet/api/system.guid), [Double](https://docs.microsoft.com/en-us/dotnet/api/system.double), [Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean), [Float](https://docs.microsoft.com/en-us/dotnet/api/system.float), [Byte](https://docs.microsoft.com/en-us/dotnet/api/system.byte), [DateTime](https://docs.microsoft.com/en-us/dotnet/api/system.datetime), [DateTimeOffset](https://docs.microsoft.com/en-us/dotnet/api/system.datetimeoffset), [Decimal](https://docs.microsoft.com/en-us/dotnet/api/system.decimal), [Int16](https://docs.microsoft.com/en-us/dotnet/api/system.int16), [Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32), [Int64](https://docs.microsoft.com/en-us/dotnet/api/system.int64), [UInt16](https://docs.microsoft.com/en-us/dotnet/api/system.uint16), [UInt32](https://docs.microsoft.com/en-us/dotnet/api/system.uint32), and [UInt64](https://docs.microsoft.com/en-us/dotnet/api/system.uint64).


##### Supported Comparisons

 * `equal` (the default value if `comparison` is omitted)
 * `notEqual`
 * `greaterThan`
 * `greaterThanOrEqual`
 * `lessThan`
 * `lessThanOrEqual`:
 * `contains`: Only works with `string`
 * `startsWith`: Only works with `string`
 * `endsWith`: Only works with `string`
 * `in`: Check if a member existing in a given collection of values
 * `notIn`: Check if a member doesn't exist in a given collection of values
 * `like`: Performs a SQL Like by using `EF.Functions.Like`

Case of comparison names are ignored. So, for example, `EndsWith`, `endsWith`, and `endswith` are  allowed.


##### Single

Single where statements can be expressed:

```graphql
{
  entities
  (where: {
    path: "Property",
    comparison: "equal",
    value: "the value"})
  {
    property
  }
}
```
