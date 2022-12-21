using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;
using Mapster;

namespace Edu.WebApi.Application.EducationProcess.ReceivedEducations.Queries.GetById;

public class GetReceivedEducationByIdQuery : IRequest<GetReceivedEducationByIdResponse>
{
    public int Id { get; set; }
}

internal class GetReceivedEducationByIdQueryHandler : IRequestHandler<GetReceivedEducationByIdQuery, GetReceivedEducationByIdResponse>
{
    private readonly IUnitOfWork<int> _unitOfWork;

    public GetReceivedEducationByIdQueryHandler(IUnitOfWork<int> unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetReceivedEducationByIdResponse> Handle(GetReceivedEducationByIdQuery query, CancellationToken cancellationToken)
    {
        var receivedEducation = await _unitOfWork.Repository<ReceivedEducation>().GetByIdAsync(query.Id);
        return receivedEducation.Adapt<GetReceivedEducationByIdResponse>();
    }
}
