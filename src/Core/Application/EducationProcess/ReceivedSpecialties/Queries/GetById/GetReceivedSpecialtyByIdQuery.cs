using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;
using Mapster;

namespace Edu.WebApi.Application.EducationProcess.ReceivedSpecialties.Queries.GetById;

public class GetReceivedSpecialtyByIdQuery : IRequest<GetReceivedSpecialtyByIdResponse>
{
    public int Id { get; set; }
}

internal class GetReceivedSpecialtyByIdQueryHandler : IRequestHandler<GetReceivedSpecialtyByIdQuery, GetReceivedSpecialtyByIdResponse>
{
    private readonly IUnitOfWork<int> _unitOfWork;

    public GetReceivedSpecialtyByIdQueryHandler(IUnitOfWork<int> unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetReceivedSpecialtyByIdResponse> Handle(GetReceivedSpecialtyByIdQuery query, CancellationToken cancellationToken)
    {
        var receivedSpecialty = await _unitOfWork.Repository<ReceivedSpecialty>().GetByIdAsync(query.Id);
        return receivedSpecialty.Adapt<GetReceivedSpecialtyByIdResponse>();
    }
}
