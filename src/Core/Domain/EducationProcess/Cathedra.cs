namespace Edu.WebApi.Domain.EducationProcess;

public class Cathedra : AuditableEntity<int>
{
    public Cathedra()
    {
        Disciplines = new HashSet<Discipline>();
        Employees = new HashSet<Employee>();
        FsesCategoryPartitions = new HashSet<FsesCategoryPartition>();
    }

    public string Name { get; set; } = null!;
    public string? NameAbbreviation { get; set; }
    public string? Description { get; set; }

    public virtual ICollection<Discipline> Disciplines { get; set; }
    public virtual ICollection<Employee> Employees { get; set; }
    public virtual ICollection<FsesCategoryPartition> FsesCategoryPartitions { get; set; }
}
