namespace Edu.WebApi.Infrastructure.Persistence.Configuration;

internal static class SchemaNames
{
    // TODO: figure out how to capitalize these only for Oracle
    public static string Auditing = nameof(Auditing); // "AUDITING";
    public static string Catalog = nameof(Catalog); // "CATALOG";
    public static string Edu = nameof(Edu); // "EDU";
    public static string Identity = nameof(Identity); // "IDENTITY";
    public static string MultiTenancy = nameof(MultiTenancy); // "MULTITENANCY";
}