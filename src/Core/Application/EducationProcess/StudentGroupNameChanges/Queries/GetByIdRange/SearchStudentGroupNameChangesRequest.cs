using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.StudentGroupNameChanges.Queries.GetByIdRange;

public class SearchStudentGroupNameChangesByIdRangeRequest : BaseFilter, IRequest<ICollection<StudentGroupNameChangeDto>>
{
    public ICollection<int> Ids { get; set; }
}

public class SearchStudentGroupNameChangesByIdRangeRequestHandler : IRequestHandler<SearchStudentGroupNameChangesByIdRangeRequest, ICollection<StudentGroupNameChangeDto>>
{
    private readonly IReadAudRepository<StudentGroupNameChange> _repository;

    public SearchStudentGroupNameChangesByIdRangeRequestHandler(IReadAudRepository<StudentGroupNameChange> repository) => _repository = repository;

    public async Task<ICollection<StudentGroupNameChangeDto>> Handle(SearchStudentGroupNameChangesByIdRangeRequest request, CancellationToken cancellationToken)
    {
        var spec = new StudentGroupNameChangesByIdRangeRequestSpec(request);
        return await _repository.ListAsync(spec, cancellationToken);
    }
}