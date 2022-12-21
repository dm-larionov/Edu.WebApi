namespace Edu.WebApi.Domain.EducationProcess;

public class Audience : AuditableEntity<int>
{
    public Audience()
    {
        DisciplineScheduleReplacements = new HashSet<DisciplineScheduleReplacement>();
        DisciplineSchedules = new HashSet<DisciplineSchedule>();
    }

    public string Number { get; set; } = null!;
    public int? EmployeeHeadId { get; set; }
    public int? AudienceTypeId { get; set; }
    public short? Capacity { get; set; }

    public virtual AudienceType? AudienceType { get; set; }
    public virtual Employee? EmployeeHead { get; set; }
    public virtual ICollection<DisciplineScheduleReplacement> DisciplineScheduleReplacements { get; set; }
    public virtual ICollection<DisciplineSchedule> DisciplineSchedules { get; set; }
}
