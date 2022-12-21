namespace Edu.WebApi.Application.EducationProcess.FixedDisciplineStatuses.Queries.GetAll;

public class GetAllFixedDisciplineStatusesResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
}
