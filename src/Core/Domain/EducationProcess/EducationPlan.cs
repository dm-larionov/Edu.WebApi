namespace Edu.WebApi.Domain.EducationProcess;

public class EducationPlan : AuditableEntity<int>
{
    public EducationPlan()
    {
        StudentGroups = new HashSet<StudentGroup>();
        SemesterDisciplines = new HashSet<DisciplineSemester>();
    }

    public int FsesCategoryPartitionId { get; set; }
    public string Name { get; set; } = null!;
    public short BeginingYear { get; set; }
    public short EndingYear { get; set; }
    public string? Description { get; set; }

    public virtual FsesCategoryPartition FsesCategoryPartition { get; set; } = null!;
    public virtual ICollection<StudentGroup> StudentGroups { get; set; }

    public virtual ICollection<DisciplineSemester> SemesterDisciplines { get; set; }
}
