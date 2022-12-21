using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;
using Mapster;

namespace Edu.WebApi.Application.EducationProcess.Audiences.Queries.GetAll;

public class GetAllAudiencesQuery : IRequest<List<GetAllAudiencesResponse>>
{
    public GetAllAudiencesQuery()
    {
    }
}

internal class GetAllAudiencesCachedQueryHandler : IRequestHandler<GetAllAudiencesQuery, List<GetAllAudiencesResponse>>
{
    private readonly IUnitOfWork<int> _unitOfWork;


    public GetAllAudiencesCachedQueryHandler(IUnitOfWork<int> unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<GetAllAudiencesResponse>> Handle(GetAllAudiencesQuery request, CancellationToken cancellationToken)
    {
        var audiences = await _unitOfWork.Repository<Audience>().GetAllAsync();
        return audiences.Adapt<List<GetAllAudiencesResponse>>();
    }
}
