namespace Edu.WebApi.Application.EducationProcess.Employees.Queries.GetAll;

public class GetAllEmployeesResponse
{
    public int Id { get; set; }
    public string Firstname { get; set; } = default!;
    public string Lastname { get; set; } = default!;
    public string? Middlename { get; set; }
    public int PostId { get; set; }
}
