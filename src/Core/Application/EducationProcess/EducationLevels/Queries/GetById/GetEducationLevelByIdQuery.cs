using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;
using Mapster;

namespace Edu.WebApi.Application.EducationProcess.EducationLevels.Queries.GetById;

public class GetEducationLevelByIdQuery : IRequest<GetEducationLevelByIdResponse>
{
    public int Id { get; set; }
}

internal class GetEducationLevelByIdQueryHandler : IRequestHandler<GetEducationLevelByIdQuery, GetEducationLevelByIdResponse>
{
    private readonly IUnitOfWork<int> _unitOfWork;

    public GetEducationLevelByIdQueryHandler(IUnitOfWork<int> unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetEducationLevelByIdResponse> Handle(GetEducationLevelByIdQuery query, CancellationToken cancellationToken)
    {
        var educationLevel = await _unitOfWork.Repository<EducationLevel>().GetByIdAsync(query.Id);
        return educationLevel.Adapt<GetEducationLevelByIdResponse>();
    }
}
