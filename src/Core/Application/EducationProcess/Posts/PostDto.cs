namespace Edu.WebApi.Application.EducationProcess.Posts;

public class PostDto : IDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
}
