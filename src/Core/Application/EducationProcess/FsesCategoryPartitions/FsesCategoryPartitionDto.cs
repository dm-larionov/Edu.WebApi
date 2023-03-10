namespace Edu.WebApi.Application.EducationProcess.FsesCategoryPartitions;

public class FsesCategoryPartitionDto : IDto
{
    public int Id { get; set; }
    public int FirstPartNumber { get; set; }
    public int SecondPartNumber { get; set; }
    public short? ThirdPathNumber { get; set; }
    public string Name { get; set; } = null!;
    public string? NameAbbreviation { get; set; }
}
