namespace Edu.WebApi.Domain.EducationProcess;

public class EducationLevel : AuditableEntity<int>
{
    public EducationLevel()
    {
        ReceivedEducations = new HashSet<ReceivedEducation>();
    }

    public string Name { get; set; } = null!;

    public virtual ICollection<ReceivedEducation> ReceivedEducations { get; set; }
}
