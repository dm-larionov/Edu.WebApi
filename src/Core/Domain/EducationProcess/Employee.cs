namespace Edu.WebApi.Domain.EducationProcess;

public class Employee : AuditableEntity<int>
{
    public Employee()
    {
        Audiences = new HashSet<Audience>();
        FixedDisciplines = new HashSet<FixedDiscipline>();
        StudentGroups = new HashSet<StudentGroup>();
        Cathedras = new HashSet<Cathedra>();
    }

    public string Firstname { get; set; } = null!;
    public string Lastname { get; set; } = null!;
    public string? Middlename { get; set; }
    public int PostId { get; set; }

    public virtual Post Post { get; set; } = null!;
    public virtual ICollection<Audience> Audiences { get; set; }
    public virtual ICollection<FixedDiscipline> FixedDisciplines { get; set; }
    public virtual ICollection<StudentGroup> StudentGroups { get; set; }

    public virtual ICollection<Cathedra> Cathedras { get; set; }
}
