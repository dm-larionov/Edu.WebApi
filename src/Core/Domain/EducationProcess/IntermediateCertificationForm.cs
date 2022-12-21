namespace Edu.WebApi.Domain.EducationProcess;

public class IntermediateCertificationForm : AuditableEntity<int>
{
    public IntermediateCertificationForm()
    {
        DisciplineSemesters = new HashSet<DisciplineSemester>();
    }

    public string Name { get; set; } = null!;

    public virtual ICollection<DisciplineSemester> DisciplineSemesters { get; set; }
}
