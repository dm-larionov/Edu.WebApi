namespace Edu.WebApi.Application.EducationProcess.FixedDisciplines;

public class FixedDisciplineDto : IDto
{
    public int Id { get; set; }
    public int FixingEmployeeId { get; set; }
    public int DisciplineSemesterId { get; set; }
    public int StudentGroupId { get; set; }
    public int FixedDisciplineStatusId { get; set; }
}
