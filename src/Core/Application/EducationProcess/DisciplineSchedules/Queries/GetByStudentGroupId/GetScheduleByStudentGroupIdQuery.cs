using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Edu.WebApi.Application.EducationProcess.DisciplineSchedules.Queries.GetByStudentGroupId;

public class GetScheduleByStudentGroupIdQuery : IRequest<ICollection<DisciplineScheduleDto>>
{
    public int StudentGroupId { get; set; }
    public DateTime Date { get; set; }
}

internal class GetDisciplineScheduleByIdQueryHandler : IRequestHandler<GetScheduleByStudentGroupIdQuery, ICollection<DisciplineScheduleDto>>
{
    private readonly IRepositoryAsync<DisciplineSchedule, int> _repository;

    public GetDisciplineScheduleByIdQueryHandler(IRepositoryAsync<DisciplineSchedule, int> repository)
    {
        _repository = repository;
    }

    public async Task<ICollection<DisciplineScheduleDto>> Handle(GetScheduleByStudentGroupIdQuery query, CancellationToken cancellationToken)
    {
        int offset = query.Date.DayOfWeek - DayOfWeek.Monday;
        var monday = query.Date.AddDays(-offset);
        if (query.Date.DayOfWeek == DayOfWeek.Sunday)
            monday = monday.AddDays(-7);
        var sunday = monday.AddDays(6);

        var disciplineSchedules = await _repository.Entities
            .Where(x =>
                x.Date >= monday.Date && x.Date <= sunday.Date && x.FixedDiscipline.StudentGroupId == query.StudentGroupId)
            .AsNoTracking()
            .ToArrayAsync(cancellationToken: cancellationToken);

        return disciplineSchedules.Adapt<List<DisciplineScheduleDto>>();
    }
}
