namespace Edu.WebApi.Application.EducationProcess.DisciplineSchedules;

public class DisciplineScheduleDto : IDto
{
    public int Id { get; set; }
    public int FixedDisciplineId { get; set; }
    public DateTime Date { get; set; }
    public int PairNumber { get; set; }
    public int? AudienceId { get; set; }
    public bool? IsEvenPair { get; set; }
    public bool? IsFirstSubgroup { get; set; }
}
