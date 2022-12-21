namespace Edu.WebApi.Application.EducationProcess.ReceivedSpecialties.Queries.GetAll;

public class GetAllReceivedSpecialtiesResponse
{
    public int Id { get; set; }
    public int FsesCategoryPartitionId { get; set; }
    public string Qualification { get; set; } = default!;
}
