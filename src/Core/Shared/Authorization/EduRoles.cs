using System.Collections.ObjectModel;

namespace Edu.WebApi.Shared.Authorization;

public static class EduRoles
{
    public const string Admin = nameof(Admin);
    public const string Basic = nameof(Basic);
    public const string HeadUmo = nameof(HeadUmo);
    public const string EmployeeUmo = nameof(EmployeeUmo);
    public const string Teacher = nameof(Teacher);

    public static IReadOnlyList<string> DefaultRoles { get; } = new ReadOnlyCollection<string>(new[]
    {
        Admin,
        Basic,
        HeadUmo,
        EmployeeUmo,
        Teacher
    });

    public static bool IsDefault(string roleName) => DefaultRoles.Any(r => r == roleName);
}