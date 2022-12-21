namespace Edu.WebApi.Application.EducationProcess.EducationModules.Queries.GetById;

public class GetEducationModuleByIdResponse
{
    public int Id { get; set; }
    public string EducationModuleIndex { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
}
