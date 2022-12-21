namespace Edu.WebApi.Domain.EducationProcess;

public class StudentGroupNameChange : AuditableEntity<int>
{
    public int StudentGroupId { get; set; }
    public string Name { get; set; } = null!;
    //TODO: Добавить время
    public DateTime Date { get; set; }

    public virtual StudentGroup StudentGroup { get; set; } = null!;
}
