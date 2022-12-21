namespace Edu.WebApi.Application.EducationProcess.EducationModules;

public class EducationModuleDto : IDto
{
    public int Id { get; set; }
    public string EducationModuleIndex { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
}
