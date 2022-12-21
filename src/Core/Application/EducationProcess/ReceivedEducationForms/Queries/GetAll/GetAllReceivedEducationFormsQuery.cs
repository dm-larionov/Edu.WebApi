using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;
using Mapster;

namespace Edu.WebApi.Application.EducationProcess.ReceivedEducationForms.Queries.GetAll;

public class GetAllReceivedEducationFormsQuery : IRequest<List<GetAllReceivedEducationFormsResponse>>
{
    public GetAllReceivedEducationFormsQuery()
    {
    }
}

internal class GetAllReceivedEducationFormsCachedQueryHandler : IRequestHandler<GetAllReceivedEducationFormsQuery, List<GetAllReceivedEducationFormsResponse>>
{
    private readonly IUnitOfWork<int> _unitOfWork;

    public GetAllReceivedEducationFormsCachedQueryHandler(IUnitOfWork<int> unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<GetAllReceivedEducationFormsResponse>> Handle(GetAllReceivedEducationFormsQuery request, CancellationToken cancellationToken)
    {
        var receivedEducationForm = await _unitOfWork.Repository<ReceivedEducationForm>().GetAllAsync();
        return receivedEducationForm.Adapt<List<GetAllReceivedEducationFormsResponse>>();
    }
}
