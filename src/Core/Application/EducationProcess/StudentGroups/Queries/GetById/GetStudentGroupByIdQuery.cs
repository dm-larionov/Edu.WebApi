using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;
using Mapster;

namespace Edu.WebApi.Application.EducationProcess.StudentGroups.Queries.GetById;

public class GetStudentGroupByIdQuery : IRequest<GetStudentGroupByIdResponse>
{
    public int Id { get; set; }
}

internal class GetStudentGroupByIdQueryHandler : IRequestHandler<GetStudentGroupByIdQuery, GetStudentGroupByIdResponse>
{
    private readonly IUnitOfWork<int> _unitOfWork;

    public GetStudentGroupByIdQueryHandler(IUnitOfWork<int> unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetStudentGroupByIdResponse> Handle(GetStudentGroupByIdQuery query, CancellationToken cancellationToken)
    {
        var studentGroup = await _unitOfWork.Repository<StudentGroup>().GetByIdAsync(query.Id);
        return studentGroup.Adapt<GetStudentGroupByIdResponse>();
    }
}
