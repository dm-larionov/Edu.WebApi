using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;
using Mapster;

namespace Edu.WebApi.Application.EducationProcess.IntermediateCertificationForms.Queries.GetById;

public class GetIntermediateCertificationFormByIdQuery : IRequest<GetIntermediateCertificationFormByIdResponse>
{
    public int Id { get; set; }
}

internal class GetIntermediateCertificationFormByIdQueryHandler : IRequestHandler<GetIntermediateCertificationFormByIdQuery, GetIntermediateCertificationFormByIdResponse>
{
    private readonly IUnitOfWork<int> _unitOfWork;

    public GetIntermediateCertificationFormByIdQueryHandler(IUnitOfWork<int> unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetIntermediateCertificationFormByIdResponse> Handle(GetIntermediateCertificationFormByIdQuery query, CancellationToken cancellationToken)
    {
        var intermediateCertificationForm = await _unitOfWork.Repository<IntermediateCertificationForm>().GetByIdAsync(query.Id);
        return intermediateCertificationForm.Adapt<GetIntermediateCertificationFormByIdResponse>();
    }
}
