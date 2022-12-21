using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.StudentGroups.Queries.Search;

public class SearchStudentGroupsRequest : PaginationFilter, IRequest<PaginationResponse<StudentGroupDto>>
{
    public byte? CourseNumber { get; set; }
    public int? FsesId { get; set; }
    public int? EducationPlanId { get; set; }
    public short? ReceiptYear { get; set; }
}

public class SearchStudentGroupsRequestHandler : IRequestHandler<SearchStudentGroupsRequest, PaginationResponse<StudentGroupDto>>
{
    private readonly IReadAudRepository<StudentGroup> _repository;

    public SearchStudentGroupsRequestHandler(IReadAudRepository<StudentGroup> repository) => _repository = repository;

    public async Task<PaginationResponse<StudentGroupDto>> Handle(SearchStudentGroupsRequest request, CancellationToken cancellationToken)
    {
        var spec = new StudentGroupsBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken: cancellationToken);
    }
}