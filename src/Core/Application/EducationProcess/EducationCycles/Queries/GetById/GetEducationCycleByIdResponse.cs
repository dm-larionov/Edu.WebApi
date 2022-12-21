namespace Edu.WebApi.Application.EducationProcess.EducationCycles.Queries.GetById;

public class GetEducationCycleByIdResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
}
