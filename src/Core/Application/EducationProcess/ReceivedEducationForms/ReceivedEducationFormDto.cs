namespace Edu.WebApi.Application.EducationProcess.ReceivedEducationForms;

public class ReceivedEducationFormDto : IDto
{
    public int Id { get; set; }
    public int EducationFormId { get; set; }
    public string? AdditionalInfo { get; set; }
}
