namespace Edu.WebApi.Application.EducationProcess.DisciplineScheduleReplacements.Queries.GetById;

public class GetDisciplineScheduleReplacementByIdResponse
{
    public int Id { get; set; }
    public int? DisciplineScheduleId { get; set; }
    public int FixedDisciplineId { get; set; }
    public DateTime Date { get; set; }
    public int PairNumber { get; set; }
    public int? AudienceId { get; set; }
    public bool? IsFirstSubgroup { get; set; }
}
