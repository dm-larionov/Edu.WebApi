namespace Edu.WebApi.Domain.EducationProcess;

public class DisciplineScheduleReplacement : AuditableEntity<int>
{
    public int? DisciplineScheduleId { get; set; }
    public int FixedDisciplineId { get; set; }
    public DateTime Date { get; set; }
    public int PairNumber { get; set; }
    public int? AudienceId { get; set; }
    public bool? IsFirstSubgroup { get; set; }

    public virtual Audience? Audience { get; set; }
    public virtual DisciplineSchedule? DisciplineSchedule { get; set; }
    public virtual FixedDiscipline FixedDiscipline { get; set; } = null!;
}
