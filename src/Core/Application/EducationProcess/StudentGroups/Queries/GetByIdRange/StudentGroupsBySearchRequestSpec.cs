using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.StudentGroups.Queries.GetByIdRange;

public class StudentGroupsByIdRangeRequestSpec : EntitiesByBaseFilterSpec<StudentGroup, StudentGroupDto>
{
    public StudentGroupsByIdRangeRequestSpec(SearchStudentGroupsByIdRangeRequest request)
        : base(request) =>
        Query.Where(x => request.Ids.Contains(x.Id));
}