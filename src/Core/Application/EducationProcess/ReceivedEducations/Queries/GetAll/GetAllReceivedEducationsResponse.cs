namespace Edu.WebApi.Application.EducationProcess.ReceivedEducations.Queries.GetAll;

public class GetAllReceivedEducationsResponse
{
    public int Id { get; set; }
    public int ReceivedSpecialtyId { get; set; }
    public int ReceivedEducationFormId { get; set; }
    public int EducationLevelId { get; set; }
    public short StudyPeriodMonths { get; set; }
    public bool IsBudget { get; set; }
}
