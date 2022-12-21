namespace Edu.WebApi.Domain.EducationProcess;

public class Discipline : AuditableEntity<int>
{
    public Discipline()
    {
        DisciplineSemesters = new HashSet<DisciplineSemester>();
    }

    public string DisciplineIndex { get; set; } = null!;
    public int? CathedraId { get; set; }
    public int EducationCycleId { get; set; }
    public int? EducationModuleId { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }

    public virtual Cathedra? Cathedra { get; set; }
    public virtual EducationCycle EducationCycle { get; set; } = null!;
    public virtual EducationModule? EducationModule { get; set; }
    public virtual ICollection<DisciplineSemester> DisciplineSemesters { get; set; }
}
