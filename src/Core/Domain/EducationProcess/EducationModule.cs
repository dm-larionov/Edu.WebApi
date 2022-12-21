namespace Edu.WebApi.Domain.EducationProcess;

public class EducationModule : AuditableEntity<int>
{
    public EducationModule()
    {
        Disciplines = new HashSet<Discipline>();
    }

    public string EducationModuleIndex { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? Description { get; set; }

    public virtual ICollection<Discipline> Disciplines { get; set; }
}
