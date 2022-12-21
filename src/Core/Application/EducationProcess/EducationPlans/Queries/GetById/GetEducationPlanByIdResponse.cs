namespace Edu.WebApi.Application.EducationProcess.EducationPlans.Queries.GetById;

public class GetEducationPlanByIdResponse
{
    public int Id { get; set; }
    public int FsesCategoryPartitionId { get; set; }
    public string Name { get; set; } = default!;
    public short BeginingYear { get; set; }
    public short EndingYear { get; set; }
    public string? Description { get; set; }
}
