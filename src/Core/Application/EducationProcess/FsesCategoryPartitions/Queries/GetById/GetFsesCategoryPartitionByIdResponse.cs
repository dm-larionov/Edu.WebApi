namespace Edu.WebApi.Application.EducationProcess.FsesCategoryPartitions.Queries.GetById;

public class GetFsesCategoryPartitionByIdResponse
{
    public int Id { get; set; }
    public int FirstPartNumber { get; set; }
    public int SecondPartNumber { get; set; }
    public short? ThirdPathNumber { get; set; }
    public string Name { get; set; } = default!;
    public string? NameAbbreviation { get; set; }
}
