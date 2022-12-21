namespace Edu.WebApi.Domain.EducationProcess;

public class AudienceType : AuditableEntity<int>
{
    public AudienceType()
    {
        Audiences = new HashSet<Audience>();
    }

    public string Name { get; set; } = null!;

    public virtual ICollection<Audience> Audiences { get; set; }
}
