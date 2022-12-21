using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;
using Mapster;

namespace Edu.WebApi.Application.EducationProcess.EducationModules.Queries.GetById;

public class GetEducationModuleByIdQuery : IRequest<GetEducationModuleByIdResponse>
{
    public int Id { get; set; }
}

internal class GetEducationModuleByIdQueryHandler : IRequestHandler<GetEducationModuleByIdQuery, GetEducationModuleByIdResponse>
{
    private readonly IUnitOfWork<int> _unitOfWork;

    public GetEducationModuleByIdQueryHandler(IUnitOfWork<int> unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetEducationModuleByIdResponse> Handle(GetEducationModuleByIdQuery query, CancellationToken cancellationToken)
    {
        var educationModule = await _unitOfWork.Repository<EducationModule>().GetByIdAsync(query.Id);
        return educationModule.Adapt<GetEducationModuleByIdResponse>();
    }
}
