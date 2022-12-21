namespace Edu.WebApi.Application.EducationProcess.EducationCycles;

public class EducationCycleDto : IDto
{
    public int Id { get; set; }
    public string EducationCycleIndex { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
}
