namespace Edu.WebApi.Domain.EducationProcess;

public class ReceivedSpecialty : AuditableEntity<int>
{
    public ReceivedSpecialty()
    {
        ReceivedEducations = new HashSet<ReceivedEducation>();
    }

    public int FsesCategoryPartitionId { get; set; }
    public string Qualification { get; set; } = null!;

    public virtual FsesCategoryPartition FsesCategoryPartition { get; set; } = null!;
    public virtual ICollection<ReceivedEducation> ReceivedEducations { get; set; }
}
