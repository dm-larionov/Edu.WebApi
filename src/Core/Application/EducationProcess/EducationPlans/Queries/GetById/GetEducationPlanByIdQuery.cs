using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;
using Mapster;

namespace Edu.WebApi.Application.EducationProcess.EducationPlans.Queries.GetById;

public class GetEducationPlanByIdQuery : IRequest<GetEducationPlanByIdResponse>
{
    public int Id { get; set; }
}

internal class GetEducationPlanByIdQueryHandler : IRequestHandler<GetEducationPlanByIdQuery, GetEducationPlanByIdResponse>
{
    private readonly IUnitOfWork<int> _unitOfWork;

    public GetEducationPlanByIdQueryHandler(IUnitOfWork<int> unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetEducationPlanByIdResponse> Handle(GetEducationPlanByIdQuery query, CancellationToken cancellationToken)
    {
        var educationPlan = await _unitOfWork.Repository<EducationPlan>().GetByIdAsync(query.Id);
        return educationPlan.Adapt<GetEducationPlanByIdResponse>();
    }
}
