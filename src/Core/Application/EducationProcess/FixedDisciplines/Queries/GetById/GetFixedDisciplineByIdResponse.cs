namespace Edu.WebApi.Application.EducationProcess.FixedDisciplines.Queries.GetById;

public class GetFixedDisciplineByIdResponse
{
    public int Id { get; set; }
    public int FixingEmployeeId { get; set; }
    public int DisciplineSemesterId { get; set; }
    public int StudentGroupId { get; set; }
    public int FixedDisciplineStatusId { get; set; }
}
