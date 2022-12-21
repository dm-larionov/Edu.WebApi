using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.StudentGroupNameChanges.Queries.GetByIdRange;

public class StudentGroupNameChangesByIdRangeRequestSpec : EntitiesByBaseFilterSpec<StudentGroupNameChange, StudentGroupNameChangeDto>
{
    public StudentGroupNameChangesByIdRangeRequestSpec(SearchStudentGroupNameChangesByIdRangeRequest request)
        : base(request) =>
        Query.Where(x => request.Ids.Contains(x.Id));
}