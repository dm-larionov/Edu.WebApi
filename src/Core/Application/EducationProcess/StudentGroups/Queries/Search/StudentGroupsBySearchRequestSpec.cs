using Edu.WebApi.Domain.EducationProcess;

namespace Edu.WebApi.Application.EducationProcess.StudentGroups.Queries.Search;

public class StudentGroupsBySearchRequestSpec : EntitiesByPaginationFilterSpec<StudentGroup, StudentGroupDto>
{
    public StudentGroupsBySearchRequestSpec(SearchStudentGroupsRequest request)
        : base(request) =>
        Query
            .Where(p => p.EducationPlanId.Equals(request.EducationPlanId!.Value), request.EducationPlanId.HasValue)
            .Where(p => p.CourseNumber.Equals(request.CourseNumber!.Value), request.CourseNumber.HasValue)
            .Where(p => p.ReceiptYear.Equals(request.ReceiptYear!.Value), request.ReceiptYear.HasValue)
            .Where(p => p.ReceivedEducation.ReceivedSpecialty.FsesCategoryPartitionId.Equals(request.FsesId!.Value), request.FsesId.HasValue);
}