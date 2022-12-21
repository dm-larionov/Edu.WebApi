namespace Edu.WebApi.Application.EducationProcess.EducationModules.Queries.GetAll;

public class GetAllEducationModulesResponse
{
    public int Id { get; set; }
    public string EducationModuleIndex { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
}
