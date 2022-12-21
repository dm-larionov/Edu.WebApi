namespace Edu.WebApi.Application.EducationProcess.EducationPlans;

public class EducationPlanDto : IDto
{
    public int Id { get; set; }
    public int FsesCategoryPartitionId { get; set; }
    public string Name { get; set; } = null!;
    public short BeginingYear { get; set; }
    public short EndingYear { get; set; }
    public string? Description { get; set; }
}
