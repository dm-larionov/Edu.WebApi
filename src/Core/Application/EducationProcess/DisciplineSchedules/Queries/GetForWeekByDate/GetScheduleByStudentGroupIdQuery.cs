using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Edu.WebApi.Application.EducationProcess.DisciplineSchedules.Queries.GetForWeekByDate;

public class GetScheduleForWeekByDateQuery : IRequest<ICollection<DisciplineScheduleDto>>
{
    public DateTime Date { get; set; }
}

internal class GetDisciplineScheduleForWeekByDateQueryHandler : IRequestHandler<GetScheduleForWeekByDateQuery, ICollection<DisciplineScheduleDto>>
{
    private readonly IDisciplineScheduleRepository _disciplineScheduleRepository;

    public GetDisciplineScheduleForWeekByDateQueryHandler(IDisciplineScheduleRepository disciplineScheduleRepository)
    {
        _disciplineScheduleRepository = disciplineScheduleRepository;
    }

    public async Task<ICollection<DisciplineScheduleDto>> Handle(GetScheduleForWeekByDateQuery query, CancellationToken cancellationToken)
    {
        return (await _disciplineScheduleRepository
            .GetDepartmentStudentGroupsScheduleForWeekAsync(query.Date))
            .Adapt<List<DisciplineScheduleDto>>();
    }
}
