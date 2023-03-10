namespace Edu.WebApi.Application.EducationProcess.DisciplineSemesters;

public class DisciplineSemesterDto : IDto
{
    public int Id { get; set; }
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
}
