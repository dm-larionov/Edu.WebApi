using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;
using Mapster;

namespace Edu.WebApi.Application.EducationProcess.EducationForms.Queries.GetById;

public class GetEducationFormByIdQuery : IRequest<GetEducationFormByIdResponse>
{
    public int Id { get; set; }
}

internal class GetEducationFormByIdQueryHandler : IRequestHandler<GetEducationFormByIdQuery, GetEducationFormByIdResponse>
{
    private readonly IUnitOfWork<int> _unitOfWork;

    public GetEducationFormByIdQueryHandler(IUnitOfWork<int> unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetEducationFormByIdResponse> Handle(GetEducationFormByIdQuery query, CancellationToken cancellationToken)
    {
        var educationForm = await _unitOfWork.Repository<EducationForm>().GetByIdAsync(query.Id);
        return educationForm.Adapt<GetEducationFormByIdResponse>();
    }
}
