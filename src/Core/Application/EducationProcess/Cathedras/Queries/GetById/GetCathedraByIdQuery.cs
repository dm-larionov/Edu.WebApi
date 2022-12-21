using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;
using Mapster;

namespace Edu.WebApi.Application.EducationProcess.Cathedras.Queries.GetById;

public class GetCathedraByIdQuery : IRequest<GetCathedraByIdResponse>
{
    public int Id { get; set; }
}

internal class GetСathedraByIdQueryHandler : IRequestHandler<GetCathedraByIdQuery, GetCathedraByIdResponse>
{
    private readonly IUnitOfWork<int> _unitOfWork;

    public GetСathedraByIdQueryHandler(IUnitOfWork<int> unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetCathedraByIdResponse> Handle(GetCathedraByIdQuery query, CancellationToken cancellationToken)
    {
        var cathedra = await _unitOfWork.Repository<Cathedra>().GetByIdAsync(query.Id);
        return cathedra.Adapt<GetCathedraByIdResponse>();
    }
}
