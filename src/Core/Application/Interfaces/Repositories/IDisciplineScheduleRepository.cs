using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.Interfaces.Repositories;

public interface IDisciplineScheduleRepository
{
    Task<DisciplineSchedule[]> GetDepartmentStudentGroupsScheduleForWeekAsync(DateTime date);
    Task<DisciplineSchedule[]> GetTeacherScheduleForWeekAsync(int teacherId, DateTime date);
    Task<DisciplineSchedule[]> GetStudentGroupScheduleForWeekAsync(int studentGroupId, DateTime date);
}
