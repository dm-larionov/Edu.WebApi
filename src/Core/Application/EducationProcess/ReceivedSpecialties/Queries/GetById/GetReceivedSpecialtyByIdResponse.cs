namespace Edu.WebApi.Application.EducationProcess.ReceivedSpecialties.Queries.GetById;

public class GetReceivedSpecialtyByIdResponse
{
    public int Id { get; set; }
    public int FsesCategoryPartitionId { get; set; }
    public string Qualification { get; set; } = default!;
}
