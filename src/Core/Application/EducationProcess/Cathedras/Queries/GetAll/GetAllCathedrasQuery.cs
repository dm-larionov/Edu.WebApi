using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;
using Mapster;

namespace Edu.WebApi.Application.EducationProcess.Cathedras.Queries.GetAll;

public class GetAllCathedrasQuery : IRequest<List<GetAllCathedrasResponse>>
{
    public GetAllCathedrasQuery()
    {
    }
}

internal class GetAllCathedrasCachedQueryHandler : IRequestHandler<GetAllCathedrasQuery, List<GetAllCathedrasResponse>>
{
    private readonly IUnitOfWork<int> _unitOfWork;

    public GetAllCathedrasCachedQueryHandler(IUnitOfWork<int> unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<GetAllCathedrasResponse>> Handle(GetAllCathedrasQuery request, CancellationToken cancellationToken)
    {
        var cathedras = await _unitOfWork.Repository<Cathedra>().GetAllAsync();
        return cathedras.Adapt<List<GetAllCathedrasResponse>>();
    }
}
