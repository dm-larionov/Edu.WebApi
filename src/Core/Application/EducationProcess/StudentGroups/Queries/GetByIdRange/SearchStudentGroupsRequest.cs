using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.StudentGroups.Queries.GetByIdRange;

public class SearchStudentGroupsByIdRangeRequest : BaseFilter, IRequest<ICollection<StudentGroupDto>>
{
    public ICollection<int> Ids { get; set; }
}

public class SearchStudentGroupsByIdRangeRequestHandler : IRequestHandler<SearchStudentGroupsByIdRangeRequest, ICollection<StudentGroupDto>>
{
    private readonly IReadAudRepository<StudentGroup> _repository;

    public SearchStudentGroupsByIdRangeRequestHandler(IReadAudRepository<StudentGroup> repository) => _repository = repository;

    public async Task<ICollection<StudentGroupDto>> Handle(SearchStudentGroupsByIdRangeRequest request, CancellationToken cancellationToken)
    {
        var spec = new StudentGroupsByIdRangeRequestSpec(request);
        return await _repository.ListAsync(spec, cancellationToken);
    }
}