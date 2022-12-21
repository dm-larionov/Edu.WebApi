using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;
using Mapster;

namespace Edu.WebApi.Application.EducationProcess.EducationForms.Queries.GetAll;

public class GetAllEducationFormsQuery : IRequest<List<GetAllEducationFormsResponse>>
{
    public GetAllEducationFormsQuery()
    {
    }
}

internal class GetAllEducationFormsCachedQueryHandler : IRequestHandler<GetAllEducationFormsQuery, List<GetAllEducationFormsResponse>>
{
    private readonly IUnitOfWork<int> _unitOfWork;

    public GetAllEducationFormsCachedQueryHandler(IUnitOfWork<int> unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<GetAllEducationFormsResponse>> Handle(GetAllEducationFormsQuery request, CancellationToken cancellationToken)
    {
        var educationForms = await _unitOfWork.Repository<EducationForm>().GetAllAsync();
        return educationForms.Adapt<List<GetAllEducationFormsResponse>>();
    }
}
