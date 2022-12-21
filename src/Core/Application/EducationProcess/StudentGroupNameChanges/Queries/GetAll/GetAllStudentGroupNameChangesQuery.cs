using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;
using Mapster;

namespace Edu.WebApi.Application.EducationProcess.StudentGroupNameChanges.Queries.GetAll;

    public class GetAllStudentGroupNameChangesQuery : IRequest<List<GetAllStudentGroupNameChangesResponse>>
    {
        public GetAllStudentGroupNameChangesQuery()
        {
        }
    }

    internal class GetAllStudentGroupNameChangesCachedQueryHandler : IRequestHandler<GetAllStudentGroupNameChangesQuery, List<GetAllStudentGroupNameChangesResponse>>
    {
        private readonly IUnitOfWork<int> _unitOfWork;

        public GetAllStudentGroupNameChangesCachedQueryHandler(IUnitOfWork<int> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<GetAllStudentGroupNameChangesResponse>> Handle(GetAllStudentGroupNameChangesQuery request, CancellationToken cancellationToken)
        {
            var studentGroupNameChanges = await _unitOfWork.Repository<StudentGroupNameChange>().GetAllAsync();
            return studentGroupNameChanges.Adapt<List<GetAllStudentGroupNameChangesResponse>>();
        }
    }
