namespace Edu.WebApi.Application.EducationProcess.Audiences;

public class AudienceDto : IDto
{
    public int Id { get; set; }
    public string Number { get; set; } = default!;
    public string? EmployeeHeadLastname { get; set; }
    public int? EmployeeHeadId { get; set; }
    public int? AudienceTypeId { get; set; }
    public short? Capacity { get; set; }
}
