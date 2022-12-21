using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;
using Mapster;

namespace Edu.WebApi.Application.EducationProcess.StudentGroups.Queries.GetAllExisting;

public class GetAllExistingStudentGroupsQuery : IRequest<List<GetAllExistingStudentGroupsResponse>>
{
    public GetAllExistingStudentGroupsQuery()
    {
    }
    public DateTime Date { get; set; }
}

internal class GetAllExistingStudentGroupsQueryHandler : IRequestHandler<GetAllExistingStudentGroupsQuery, List<GetAllExistingStudentGroupsResponse>>
{
    private readonly IStudentGroupRepository _repository;

    public GetAllExistingStudentGroupsQueryHandler(IStudentGroupRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<GetAllExistingStudentGroupsResponse>> Handle(GetAllExistingStudentGroupsQuery request, CancellationToken cancellationToken)
    {
        var studentGroups = await _repository.GetAllExistingByDate(request.Date);
        return studentGroups.Adapt<List<GetAllExistingStudentGroupsResponse>>();
    }
}
