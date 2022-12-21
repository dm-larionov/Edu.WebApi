namespace Edu.WebApi.Domain.EducationProcess;

public class FixedDisciplineStatus : AuditableEntity<int>
{
    public FixedDisciplineStatus()
    {
        FixedDisciplines = new HashSet<FixedDiscipline>();
    }

    public string Name { get; set; } = null!;
    public string? Description { get; set; }

    public virtual ICollection<FixedDiscipline> FixedDisciplines { get; set; }
}
