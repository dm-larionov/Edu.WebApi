namespace Edu.WebApi.Domain.EducationProcess;

public class FsesCategoryPartition : AuditableEntity<int>
{
    public FsesCategoryPartition()
    {
        EducationPlans = new HashSet<EducationPlan>();
        ReceivedSpecialties = new HashSet<ReceivedSpecialty>();
        Cathedras = new HashSet<Cathedra>();
    }

    public int FirstPartNumber { get; set; }
    public int SecondPartNumber { get; set; }
    public short? ThirdPathNumber { get; set; }
    public string Name { get; set; } = null!;
    public string? NameAbbreviation { get; set; }

    public virtual ICollection<EducationPlan> EducationPlans { get; set; }
    public virtual ICollection<ReceivedSpecialty> ReceivedSpecialties { get; set; }

    public virtual ICollection<Cathedra> Cathedras { get; set; }
}
