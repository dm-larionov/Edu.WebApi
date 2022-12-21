namespace Edu.WebApi.Domain.EducationProcess;

public class StudentGroup : AuditableEntity<int>
{
    public StudentGroup()
    {
        FixedDisciplines = new HashSet<FixedDiscipline>();
        StudentGroupNameChanges = new HashSet<StudentGroupNameChange>();
    }

    public string Name { get; set; } = null!;
    public byte CourseNumber { get; set; }
    public int? CuratorId { get; set; }
    public int ReceivedEducationId { get; set; }
    public int? EducationPlanId { get; set; }
    public short ReceiptYear { get; set; }
    public byte StudentQuantity { get; set; }

    public virtual Employee? Curator { get; set; }
    public virtual EducationPlan? EducationPlan { get; set; }
    public virtual ReceivedEducation ReceivedEducation { get; set; } = null!;
    public virtual ICollection<FixedDiscipline> FixedDisciplines { get; set; }
    public virtual ICollection<StudentGroupNameChange> StudentGroupNameChanges { get; set; }
}
