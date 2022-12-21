namespace Edu.WebApi.Application.EducationProcess.Disciplines;

public class DisciplineDto : IDto
{
    public int Id { get; set; }
    public string DisciplineIndex { get; set; } = null!;
    public int? CathedraId { get; set; }
    public int EducationCycleId { get; set; }
    public int? EducationModuleId { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
}
