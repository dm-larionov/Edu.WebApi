namespace Edu.WebApi.Domain.EducationProcess;

public class DisciplineSchedule : AuditableEntity<int>
{
    public DisciplineSchedule()
    {
        DisciplineScheduleReplacements = new HashSet<DisciplineScheduleReplacement>();
    }

    public int FixedDisciplineId { get; set; }
    public DateTime Date { get; set; }
    public int PairNumber { get; set; }
    public int? AudienceId { get; set; }
    public bool? IsEvenPair { get; set; }
    public bool? IsFirstSubgroup { get; set; }

    public virtual Audience? Audience { get; set; }
    public virtual FixedDiscipline FixedDiscipline { get; set; } = null!;
    public virtual ICollection<DisciplineScheduleReplacement> DisciplineScheduleReplacements { get; set; }
}
