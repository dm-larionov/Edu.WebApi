namespace Edu.WebApi.Application.EducationProcess.DisciplineSchedules.Queries.GetById;

public class GetDisciplineScheduleByIdResponse
{
    public int Id { get; set; }
    public int FixedDisciplineId { get; set; }
    public DateTime Date { get; set; }
    public int PairNumber { get; set; }
    public int? AudienceId { get; set; }
    public bool? IsEvenPair { get; set; }
    public bool? IsFirstSubgroup { get; set; }
}
