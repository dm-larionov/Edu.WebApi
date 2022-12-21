using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;
using Mapster;

namespace Edu.WebApi.Application.EducationProcess.StudentGroups.Queries.GetAll;

public class GetAllStudentGroupsQuery : IRequest<List<GetAllStudentGroupsResponse>>
{
    public GetAllStudentGroupsQuery()
    {
    }
}

internal class GetAllStudentGroupsCachedQueryHandler : IRequestHandler<GetAllStudentGroupsQuery, List<GetAllStudentGroupsResponse>>
{
    private readonly IUnitOfWork<int> _unitOfWork;

    public GetAllStudentGroupsCachedQueryHandler(IUnitOfWork<int> unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<GetAllStudentGroupsResponse>> Handle(GetAllStudentGroupsQuery request, CancellationToken cancellationToken)
    {
        var studentGroups = await _unitOfWork.Repository<StudentGroup>().GetAllAsync();
        return studentGroups.Adapt<List<GetAllStudentGroupsResponse>>();
    }
}
