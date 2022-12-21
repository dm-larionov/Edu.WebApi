namespace Edu.WebApi.Application.EducationProcess.AudienceTypes;

public class AudienceTypeDto : IDto
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
}
