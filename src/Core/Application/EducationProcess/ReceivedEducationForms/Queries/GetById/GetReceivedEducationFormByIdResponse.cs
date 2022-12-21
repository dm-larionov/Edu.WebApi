namespace Edu.WebApi.Application.EducationProcess.ReceivedEducationForms.Queries.GetById;

public class GetReceivedEducationFormByIdResponse
{
    public int Id { get; set; }
    public int EducationFormId { get; set; }
    public string? AdditionalInfo { get; set; }
}
