namespace Edu.WebApi.Application.EducationProcess.Employees;

public class EmployeeDto : IDto
{
    public int Id { get; set; }
    public string Firstname { get; set; } = null!;
    public string Lastname { get; set; } = null!;
    public string? Middlename { get; set; }
    public int PostId { get; set; }
}
