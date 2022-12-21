namespace Edu.WebApi.Domain.EducationProcess;

public class Post : AuditableEntity<int>
{
    public Post()
    {
        Employees = new HashSet<Employee>();
    }

    public string Name { get; set; } = null!;

    public virtual ICollection<Employee> Employees { get; set; }
}
