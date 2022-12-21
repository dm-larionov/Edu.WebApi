using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;
using Mapster;

namespace Edu.WebApi.Application.EducationProcess.DisciplineSchedules.Queries.GetById;

public class GetDisciplineScheduleByIdQuery : IRequest<GetDisciplineScheduleByIdResponse>
{
    public int Id { get; set; }
}

internal class GetDisciplineScheduleByIdQueryHandler : IRequestHandler<GetDisciplineScheduleByIdQuery, GetDisciplineScheduleByIdResponse>
{
    private readonly IUnitOfWork<int> _unitOfWork;

    public GetDisciplineScheduleByIdQueryHandler(IUnitOfWork<int> unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetDisciplineScheduleByIdResponse> Handle(GetDisciplineScheduleByIdQuery query, CancellationToken cancellationToken)
    {
        var disciplineSchedule = await _unitOfWork.Repository<DisciplineSchedule>().GetByIdAsync(query.Id);
        return disciplineSchedule.Adapt<GetDisciplineScheduleByIdResponse>();
    }
}
