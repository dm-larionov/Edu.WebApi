using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.StudentGroupNameChanges.Queries.Search;

public class StudentGroupNameChangesBySearchRequestSpec : EntitiesByPaginationFilterSpec<StudentGroupNameChange, StudentGroupNameChangeDto>
{
    public StudentGroupNameChangesBySearchRequestSpec(SearchStudentGroupNameChangesRequest request)
        : base(request) =>
        Query
            .Where(p => true);
}
