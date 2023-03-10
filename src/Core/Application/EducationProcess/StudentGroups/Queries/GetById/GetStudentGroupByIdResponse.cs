namespace Edu.WebApi.Application.EducationProcess.StudentGroups.Queries.GetById;

public class GetStudentGroupByIdResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public byte CourseNumber { get; set; }
    public int? CuratorId { get; set; }
    public int ReceivedEducationId { get; set; }
    public int? EducationPlanId { get; set; }
    public short ReceiptYear { get; set; }
    public byte StudentQuantity { get; set; }
}
