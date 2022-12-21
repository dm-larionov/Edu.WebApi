using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;
using Mapster;

namespace Edu.WebApi.Application.EducationProcess.FixedDisciplineStatuses.Queries.GetById;

public class GetFixedDisciplineStatusByIdQuery : IRequest<GetFixedDisciplineStatusByIdResponse>
{
    public int Id { get; set; }
}

internal class GetFixedDisciplineStatusByIdQueryHandler : IRequestHandler<GetFixedDisciplineStatusByIdQuery, GetFixedDisciplineStatusByIdResponse>
{
    private readonly IUnitOfWork<int> _unitOfWork;

    public GetFixedDisciplineStatusByIdQueryHandler(IUnitOfWork<int> unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetFixedDisciplineStatusByIdResponse> Handle(GetFixedDisciplineStatusByIdQuery query, CancellationToken cancellationToken)
    {
        var fixedDisciplineStatus = await _unitOfWork.Repository<FixedDiscipline>().GetByIdAsync(query.Id);
        return fixedDisciplineStatus.Adapt<GetFixedDisciplineStatusByIdResponse>();
    }
}
