namespace Edu.WebApi.Application.EducationProcess.FixedDisciplineStatuses.Queries.GetById;

public class GetFixedDisciplineStatusByIdResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
}
