namespace Edu.WebApi.Application.EducationProcess.FixedDisciplineStatuses;

public class FixedDisciplineStatusDto : IDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
}
