using System.Globalization;
using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;
using Microsoft.EntityFrameworkCore;

namespace Edu.WebApi.Infrastructure.Repositories;

public class DisciplineScheduleRepository : IDisciplineScheduleRepository
{
    private readonly IRepositoryAsync<DisciplineSchedule, int> _repository;

    public DisciplineScheduleRepository(IRepositoryAsync<DisciplineSchedule, int> repository)
    {
        _repository = repository;
    }

    public async Task<DisciplineSchedule[]> GetDepartmentStudentGroupsScheduleForWeekAsync(DateTime date)
    {
        // lastMonday is always the Monday before nextSunday.
        // When date is a Sunday, lastMonday will be tomorrow.
        int offset = date.DayOfWeek - DayOfWeek.Monday;
        var monday = date.AddDays(-offset);
        if (date.DayOfWeek == DayOfWeek.Sunday)
            monday = monday.AddDays(-7);
        var sunday = monday.AddDays(6);

        return await _repository.Entities
            .Where(x =>
                x.Date >= monday.Date && x.Date <= sunday.Date)
            .AsNoTracking()
            .ToArrayAsync();
    }

    public async Task<DisciplineSchedule[]> GetTeacherScheduleForWeekAsync(int teacherId, DateTime date)
    {
        date = AddDaysToSunday(date);

        bool isFirstHalfYear = date.Month / 7.0 < 1;

        DisciplineSchedule[] disciplineSchedules = await _repository.Entities
            .Where(x =>
                x.FixedDiscipline.FixedDisciplineStatus.Name == "Закреплено" == true &&
                x.FixedDiscipline.SemesterDiscipline.SemesterNumber == (date.Year - x.FixedDiscipline.Group.ReceiptYear + (isFirstHalfYear ? 0 : 1)) * 2 - (isFirstHalfYear ? 0 : 1) &&
                x.Date <= date &&
                x.FixedDiscipline.FixingEmployeeId == teacherId)
            .AsNoTracking()
            .ToArrayAsync();

        return await GetLastDisciplineScheduleForWeekAsync(disciplineSchedules);
    }

    // Вывод расписания за неделю.
    // Если сегодня воскресенье, выдает расписание за следующую (если таковая будет)
    public async Task<DisciplineSchedule[]> GetStudentGroupScheduleForWeekAsync(int studentGroupId, DateTime date)
    {
        if (date.DayOfWeek is DayOfWeek.Sunday)
            date = date.AddDays(7);
        date = AddDaysToSunday(date);

        bool isFirstHalfYear = date.Month / 7.0 < 1;

        DisciplineSchedule[] disciplineSchedules = await _repository.Entities
             .Where(x =>
                 x.FixedDiscipline.FixedDisciplineStatus.Name == "Закреплено" &&
                 x.FixedDiscipline.Group.Id == studentGroupId &&
                 x.FixedDiscipline.SemesterDiscipline.SemesterNumber == (date.Year - x.FixedDiscipline.Group.ReceiptYear + (isFirstHalfYear ? 0 : 1)) * 2 - (isFirstHalfYear ? 0 : 1) &&
                 x.Date <= date)
             .AsNoTracking()
             .ToArrayAsync();

        return await GetLastDisciplineScheduleForWeekAsync(disciplineSchedules);
    }

    private static async Task<DisciplineSchedule[]> GetLastDisciplineScheduleForWeekAsync(DisciplineSchedule[] disciplineSchedules)
    {
        await Task.Run(() =>
        {
            CultureInfo myCI = new CultureInfo("ru-RU");
            Calendar myCal = myCI.Calendar;

            CalendarWeekRule myCWR = myCI.DateTimeFormat.CalendarWeekRule;
            DayOfWeek myFirstDOW = myCI.DateTimeFormat.FirstDayOfWeek;

            disciplineSchedules = disciplineSchedules.OrderByDescending(x => x.Date).ToArray();
            disciplineSchedules = disciplineSchedules.Where(x =>
                    myCal.GetWeekOfYear(x.Date, myCWR, myFirstDOW) ==
                    myCal.GetWeekOfYear(
                        disciplineSchedules.FirstOrDefault(y => y.Id == x.Id)!.Date,
                        myCWR, myFirstDOW))
                .ToArray();
        });
        return disciplineSchedules;
    }

    private DateTime AddDaysToSunday(DateTime date)
    {
        if (date.DayOfWeek is not DayOfWeek.Sunday)
            date = date.AddDays(7 - (int)date.DayOfWeek);
        return date;
    }
}
