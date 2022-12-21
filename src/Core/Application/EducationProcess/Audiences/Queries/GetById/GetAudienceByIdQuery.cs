using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;
using Mapster;

namespace Edu.WebApi.Application.EducationProcess.Audiences.Queries.GetById;

public class GetAudienceByIdQuery : IRequest<GetAudienceByIdResponse>
{
    public int Id { get; set; }
}

internal class GetAudienceByIdQueryHandler : IRequestHandler<GetAudienceByIdQuery, GetAudienceByIdResponse>
{
    private readonly IUnitOfWork<int> _unitOfWork;

    public GetAudienceByIdQueryHandler(IUnitOfWork<int> unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetAudienceByIdResponse> Handle(GetAudienceByIdQuery query, CancellationToken cancellationToken)
    {
        var audience = await _unitOfWork.Repository<Audience>().GetByIdAsync(query.Id);
        return audience.Adapt<GetAudienceByIdResponse>();
    }
}
