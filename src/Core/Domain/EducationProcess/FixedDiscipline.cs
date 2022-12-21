namespace Edu.WebApi.Domain.EducationProcess;

public class FixedDiscipline : AuditableEntity<int>
{
    public FixedDiscipline()
    {
        DisciplineScheduleReplacements = new HashSet<DisciplineScheduleReplacement>();
        DisciplineSchedules = new HashSet<DisciplineSchedule>();
    }

    public int FixingEmployeeId { get; set; }
    public int DisciplineSemesterId { get; set; }
    //TODO: Change everything to StudentGroup
    public int StudentGroupId { get; set; }
    public int FixedDisciplineStatusId { get; set; }

    public virtual FixedDisciplineStatus FixedDisciplineStatus { get; set; } = null!;
    public virtual Employee FixingEmployee { get; set; } = null!;
    public virtual StudentGroup Group { get; set; } = null!;
    public virtual DisciplineSemester SemesterDiscipline { get; set; } = null!;
    public virtual ICollection<DisciplineScheduleReplacement> DisciplineScheduleReplacements { get; set; }
    public virtual ICollection<DisciplineSchedule> DisciplineSchedules { get; set; }
}
