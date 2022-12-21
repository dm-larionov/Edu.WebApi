using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Edu.WebApi.Application.EducationProcess.DisciplineSchedules.Queries.GetByTeacherId;

public class GetScheduleByTeacherIdQuery : IRequest<ICollection<DisciplineScheduleDto>>
{
    public int TeacherId { get; set; }
    public DateTime Date { get; set; }
}

internal class GetDisciplineScheduleByIdQueryHandler : IRequestHandler<GetScheduleByTeacherIdQuery, ICollection<DisciplineScheduleDto>>
{
    private readonly IRepositoryAsync<DisciplineSchedule, int> _repository;

    public GetDisciplineScheduleByIdQueryHandler(IRepositoryAsync<DisciplineSchedule, int> repository)
    {
        _repository = repository;
    }

    public async Task<ICollection<DisciplineScheduleDto>> Handle(GetScheduleByTeacherIdQuery query, CancellationToken cancellationToken)
    {
        int offset = query.Date.DayOfWeek - DayOfWeek.Monday;
        var monday = query.Date.AddDays(-offset);
        if (query.Date.DayOfWeek == DayOfWeek.Sunday)
            monday = monday.AddDays(-7);
        var sunday = monday.AddDays(6);

        var disciplineSchedules = await _repository.Entities
            .Where(x =>
                x.FixedDiscipline.FixedDisciplineStatusId == 1 &&
                //x.FixedDiscipline.SemesterDiscipline.SemesterNumber == (date.Year - x.FixedDiscipline.Group.ReceiptYear + (isFirstHalfYear ? 0 : 1)) * 2 - (isFirstHalfYear ? 0 : 1) &&
                x.Date >= monday.Date && x.Date <= sunday.Date &&
                x.FixedDiscipline.FixingEmployeeId == query.TeacherId)
            .AsNoTracking()
            .ToArrayAsync();

        return disciplineSchedules.Adapt<List<DisciplineScheduleDto>>();
    }
}
