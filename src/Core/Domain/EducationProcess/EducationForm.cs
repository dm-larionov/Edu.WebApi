namespace Edu.WebApi.Domain.EducationProcess;

public class EducationForm : AuditableEntity<int>
{
    public EducationForm()
    {
        ReceivedEducationForms = new HashSet<ReceivedEducationForm>();
    }

    public string Name { get; set; } = null!;

    public virtual ICollection<ReceivedEducationForm> ReceivedEducationForms { get; set; }
}
