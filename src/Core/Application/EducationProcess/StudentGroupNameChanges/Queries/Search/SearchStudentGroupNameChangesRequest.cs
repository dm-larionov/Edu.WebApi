using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.StudentGroupNameChanges.Queries.Search;

public class SearchStudentGroupNameChangesRequest : PaginationFilter, IRequest<PaginationResponse<StudentGroupNameChangeDto>>
{
}

public class SearchStudentGroupNameChangesRequestHandler : IRequestHandler<SearchStudentGroupNameChangesRequest, PaginationResponse<StudentGroupNameChangeDto>>
{
    private readonly IReadAudRepository<StudentGroupNameChange> _repository;

    public SearchStudentGroupNameChangesRequestHandler(IReadAudRepository<StudentGroupNameChange> repository) => _repository = repository;

    public async Task<PaginationResponse<StudentGroupNameChangeDto>> Handle(SearchStudentGroupNameChangesRequest request, CancellationToken cancellationToken)
    {
        var spec = new StudentGroupNameChangesBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken: cancellationToken);
    }
}
