namespace Edu.WebApi.Domain.EducationProcess;

public class ReceivedEducationForm : AuditableEntity<int>
{
    public ReceivedEducationForm()
    {
        ReceivedEducations = new HashSet<ReceivedEducation>();
    }

    public int EducationFormId { get; set; }
    public string? AdditionalInfo { get; set; }

    public virtual EducationForm EducationForm { get; set; } = null!;
    public virtual ICollection<ReceivedEducation> ReceivedEducations { get; set; }
}
