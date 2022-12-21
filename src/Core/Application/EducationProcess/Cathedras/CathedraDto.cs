namespace Edu.WebApi.Application.EducationProcess.Cathedras;

public class CathedraDto : IDto
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string NameAbbreviation { get; set; } = default!;
    public string? Description { get; set; }
}
