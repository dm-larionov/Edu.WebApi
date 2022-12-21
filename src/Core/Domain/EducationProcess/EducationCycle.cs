namespace Edu.WebApi.Domain.EducationProcess;

public class EducationCycle : AuditableEntity<int>
{
    public EducationCycle()
    {
        Disciplines = new HashSet<Discipline>();
    }

    public string EducationCycleIndex { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? Description { get; set; }

    public virtual ICollection<Discipline> Disciplines { get; set; }
}
