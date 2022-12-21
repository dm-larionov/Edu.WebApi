using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;
using Mapster;

namespace Edu.WebApi.Application.EducationProcess.AudienceTypes.Queries.GetById;

public class GetAudienceTypeByIdQuery : IRequest<GetAudienceTypeByIdResponse>
{
    public int Id { get; set; }
}

internal class GetAudienceTypeByIdQueryHandler : IRequestHandler<GetAudienceTypeByIdQuery, GetAudienceTypeByIdResponse>
{
    private readonly IUnitOfWork<int> _unitOfWork;

    public GetAudienceTypeByIdQueryHandler(IUnitOfWork<int> unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetAudienceTypeByIdResponse> Handle(GetAudienceTypeByIdQuery query, CancellationToken cancellationToken)
    {
        var audienceType = await _unitOfWork.Repository<AudienceType>().GetByIdAsync(query.Id);
        return audienceType.Adapt<GetAudienceTypeByIdResponse>();
    }
}
