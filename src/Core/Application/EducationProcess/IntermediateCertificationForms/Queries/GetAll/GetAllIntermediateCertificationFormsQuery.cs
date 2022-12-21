using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;
using Mapster;

namespace Edu.WebApi.Application.EducationProcess.IntermediateCertificationForms.Queries.GetAll;

public class GetAllIntermediateCertificationFormsQuery : IRequest<List<GetAllIntermediateCertificationFormsResponse>>
{
    public GetAllIntermediateCertificationFormsQuery()
    {
    }
}

internal class GetAllIntermediateCertificationFormsCachedQueryHandler : IRequestHandler<GetAllIntermediateCertificationFormsQuery, List<GetAllIntermediateCertificationFormsResponse>>
{
    private readonly IUnitOfWork<int> _unitOfWork;

    public GetAllIntermediateCertificationFormsCachedQueryHandler(IUnitOfWork<int> unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<GetAllIntermediateCertificationFormsResponse>> Handle(GetAllIntermediateCertificationFormsQuery request, CancellationToken cancellationToken)
    {
        var intermediateCertificationForms = await _unitOfWork.Repository<IntermediateCertificationForm>().GetAllAsync();
        return intermediateCertificationForms.Adapt<List<GetAllIntermediateCertificationFormsResponse>>();
    }
}
