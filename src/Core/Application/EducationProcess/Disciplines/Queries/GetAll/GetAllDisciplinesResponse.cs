namespace Edu.WebApi.Application.EducationProcess.Disciplines.Queries.GetAll;

public class GetAllDisciplinesResponse
{
    public int Id { get; set; }
    public string DisciplineIndex { get; set; } = default!;
    public int? CathedraId { get; set; }
    public int EducationCycleId { get; set; }
    public int? EducationModuleId { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
}
