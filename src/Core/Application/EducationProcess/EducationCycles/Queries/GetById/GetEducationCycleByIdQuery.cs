using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;
using Mapster;

namespace Edu.WebApi.Application.EducationProcess.EducationCycles.Queries.GetById;

public class GetEducationCycleByIdQuery : IRequest<GetEducationCycleByIdResponse>
{
    public int Id { get; set; }
}

internal class GetEducationCycleByIdQueryHandler : IRequestHandler<GetEducationCycleByIdQuery, GetEducationCycleByIdResponse>
{
    private readonly IUnitOfWork<int> _unitOfWork;

    public GetEducationCycleByIdQueryHandler(IUnitOfWork<int> unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetEducationCycleByIdResponse> Handle(GetEducationCycleByIdQuery query, CancellationToken cancellationToken)
    {
        var educationCyclesAndModule = await _unitOfWork.Repository<EducationCycle>().GetByIdAsync(query.Id);
        return educationCyclesAndModule.Adapt<GetEducationCycleByIdResponse>();
    }
}
