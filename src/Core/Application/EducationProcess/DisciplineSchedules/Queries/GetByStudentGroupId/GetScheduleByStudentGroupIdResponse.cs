namespace Edu.WebApi.Application.EducationProcess.DisciplineSchedules.Queries.GetByStudentGroupId;

public class GetScheduleByStudentGroupIdResponse
{
    public int FixedDisciplineId { get; set; }
    public DateTime Date { get; set; }
    public int PairNumber { get; set; }
    public int? AudienceId { get; set; }
    public bool? IsEvenPair { get; set; }
    public bool? IsFirstSubgroup { get; set; }

    public string AudienceNumber { get; set; } = default!;
    public string DisciplineName { get; set; } = default!;
    public string EmployeeName { get; set; } = default!;
}
