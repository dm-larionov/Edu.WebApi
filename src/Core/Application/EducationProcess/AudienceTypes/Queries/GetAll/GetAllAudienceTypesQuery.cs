using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;
using Mapster;

namespace Edu.WebApi.Application.EducationProcess.AudienceTypes.Queries.GetAll;

public class GetAllAudienceTypesQuery : IRequest<List<GetAllAudienceTypesResponse>>
{
    public GetAllAudienceTypesQuery()
    {
    }
}

internal class GetAllAudienceTypesCachedQueryHandler : IRequestHandler<GetAllAudienceTypesQuery, List<GetAllAudienceTypesResponse>>
{
    private readonly IUnitOfWork<int> _unitOfWork;

    public GetAllAudienceTypesCachedQueryHandler(IUnitOfWork<int> unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<GetAllAudienceTypesResponse>> Handle(GetAllAudienceTypesQuery request, CancellationToken cancellationToken)
    {
        List<AudienceType> audienceTypes = await _unitOfWork.Repository<AudienceType>().GetAllAsync();
        return audienceTypes.Adapt<List<GetAllAudienceTypesResponse>>();
    }
}
