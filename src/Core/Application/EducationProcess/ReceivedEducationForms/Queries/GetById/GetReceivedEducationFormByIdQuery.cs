using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;
using Mapster;

namespace Edu.WebApi.Application.EducationProcess.ReceivedEducationForms.Queries.GetById;

public class GetReceivedEducationFormByIdQuery : IRequest<GetReceivedEducationFormByIdResponse>
{
    public int Id { get; set; }
}

internal class GetReceivedEducationFormByIdQueryHandler : IRequestHandler<GetReceivedEducationFormByIdQuery, GetReceivedEducationFormByIdResponse>
    {
    private readonly IUnitOfWork<int> _unitOfWork;

    public GetReceivedEducationFormByIdQueryHandler(IUnitOfWork<int> unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetReceivedEducationFormByIdResponse> Handle(GetReceivedEducationFormByIdQuery query, CancellationToken cancellationToken)
    {
        var receivedEducationForm = await _unitOfWork.Repository<ReceivedEducationForm>().GetByIdAsync(query.Id);
        return receivedEducationForm.Adapt<GetReceivedEducationFormByIdResponse>();
    }
}
