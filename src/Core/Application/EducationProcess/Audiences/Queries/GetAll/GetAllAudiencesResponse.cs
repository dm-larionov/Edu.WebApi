namespace Edu.WebApi.Application.EducationProcess.Audiences.Queries.GetAll;

public class GetAllAudiencesResponse
{
    public int Id { get; set; }
    public string Number { get; set; } = default!;
    public int? EmployeeHeadId { get; set; }
    public int? AudienceTypeId { get; set; }
    public short? Capacity { get; set; }
}
