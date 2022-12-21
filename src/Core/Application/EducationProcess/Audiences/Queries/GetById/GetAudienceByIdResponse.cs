namespace Edu.WebApi.Application.EducationProcess.Audiences.Queries.GetById;

public class GetAudienceByIdResponse
{
    public int Id { get; set; }
    public string Number { get; set; } = default!;
    public int? EmployeeHeadId { get; set; }
    public int? AudienceTypeId { get; set; }
    public short? Capacity { get; set; }
}
