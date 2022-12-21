using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;
using Mapster;

namespace Edu.WebApi.Application.EducationProcess.StudentGroupNameChanges.Queries.GetById;

public class GetStudentGroupNameChangeByIdQuery : IRequest<GetStudentGroupNameChangeByIdResponse>
{
    public int Id { get; set; }
}

internal class GetStudentGroupNameChangeByIdQueryHandler : IRequestHandler<GetStudentGroupNameChangeByIdQuery, GetStudentGroupNameChangeByIdResponse>
{
    private readonly IUnitOfWork<int> _unitOfWork;

    public GetStudentGroupNameChangeByIdQueryHandler(IUnitOfWork<int> unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetStudentGroupNameChangeByIdResponse> Handle(GetStudentGroupNameChangeByIdQuery query, CancellationToken cancellationToken)
    {
        var studentGroupNameChange = await _unitOfWork.Repository<StudentGroupNameChange>().GetByIdAsync(query.Id);
        return studentGroupNameChange.Adapt<GetStudentGroupNameChangeByIdResponse>();
    }
}
