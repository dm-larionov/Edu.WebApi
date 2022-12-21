namespace Edu.WebApi.Application.EducationProcess.ReceivedSpecialties;

public class ReceivedSpecialtyDto : IDto
{
    public int Id { get; set; }
    public int FsesCategoryPartitionId { get; set; }
    public string Qualification { get; set; } = null!;
}
