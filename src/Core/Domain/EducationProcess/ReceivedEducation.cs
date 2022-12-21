namespace Edu.WebApi.Domain.EducationProcess;

public class ReceivedEducation : AuditableEntity<int>
{
    public ReceivedEducation()
    {
        StudentGroups = new HashSet<StudentGroup>();
    }

    public int ReceivedSpecialtyId { get; set; }
    public int ReceivedEducationFormId { get; set; }
    public int EducationLevelId { get; set; }
    public short StudyPeriodMonths { get; set; }
    public bool IsBudget { get; set; }

    public virtual EducationLevel EducationLevel { get; set; } = null!;
    public virtual ReceivedEducationForm ReceivedEducationForm { get; set; } = null!;
    public virtual ReceivedSpecialty ReceivedSpecialty { get; set; } = null!;
    public virtual ICollection<StudentGroup> StudentGroups { get; set; }
}
