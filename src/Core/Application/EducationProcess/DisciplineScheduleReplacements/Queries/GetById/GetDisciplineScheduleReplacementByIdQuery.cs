using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;
using Mapster;

namespace Edu.WebApi.Application.EducationProcess.DisciplineScheduleReplacements.Queries.GetById;

public class GetDisciplineScheduleReplacementByIdQuery : IRequest<GetDisciplineScheduleReplacementByIdResponse>
{
    public int Id { get; set; }
}

internal class GetDisciplineScheduleReplacementByIdQueryHandler : IRequestHandler<GetDisciplineScheduleReplacementByIdQuery, GetDisciplineScheduleReplacementByIdResponse>
{
    private readonly IUnitOfWork<int> _unitOfWork;

    public GetDisciplineScheduleReplacementByIdQueryHandler(IUnitOfWork<int> unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetDisciplineScheduleReplacementByIdResponse> Handle(GetDisciplineScheduleReplacementByIdQuery query, CancellationToken cancellationToken)
    {
        var disciplineScheduleReplacement = await _unitOfWork.Repository<DisciplineScheduleReplacement>().GetByIdAsync(query.Id);
        return disciplineScheduleReplacement.Adapt<GetDisciplineScheduleReplacementByIdResponse>();
    }
}
