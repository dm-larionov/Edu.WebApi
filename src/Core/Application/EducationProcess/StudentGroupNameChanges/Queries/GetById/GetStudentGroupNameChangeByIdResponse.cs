namespace Edu.WebApi.Application.EducationProcess.StudentGroupNameChanges.Queries.GetById;

public class GetStudentGroupNameChangeByIdResponse
{
    public int Id { get; set; }
    public int StudentGroupId { get; set; }
    public string Name { get; set; } = null!;
    public DateTime Date { get; set; }
}
