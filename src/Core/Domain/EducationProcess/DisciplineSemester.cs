namespace Edu.WebApi.Domain.EducationProcess;

public class DisciplineSemester : AuditableEntity<int>
{
    public DisciplineSemester()
    {
        FixedDisciplines = new HashSet<FixedDiscipline>();
        EducationPlans = new HashSet<EducationPlan>();
    }

    public byte SemesterNumber { get; set; }
    public byte? WeeksCount { get; set; }
    public int DisciplineId { get; set; }
    public short TheoryLessonHours { get; set; }
    public short PracticeWorkHours { get; set; }
    public short LaboratoryWorkHours { get; set; }
    public short ControlWorkHours { get; set; }
    public short IndependentWorkHours { get; set; }
    public short ConsultationHours { get; set; }
    public short ExamHours { get; set; }
    public short EducationalPracticeHours { get; set; }
    public short ProductionPracticeHours { get; set; }
    public int? CertificationFormId { get; set; }
    public string? Description { get; set; }

    public virtual IntermediateCertificationForm? CertificationForm { get; set; }
    public virtual Discipline Discipline { get; set; } = null!;
    public virtual ICollection<FixedDiscipline> FixedDisciplines { get; set; }
    public virtual ICollection<EducationPlan> EducationPlans { get; set; }
}
