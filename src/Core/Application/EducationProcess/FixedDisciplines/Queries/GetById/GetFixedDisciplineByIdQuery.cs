using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;
using Mapster;

namespace Edu.WebApi.Application.EducationProcess.FixedDisciplines.Queries.GetById;

public class GetFixedDisciplineByIdQuery : IRequest<GetFixedDisciplineByIdResponse>
{
    public int Id { get; set; }
}

internal class GetFixedDisciplineByIdQueryHandler : IRequestHandler<GetFixedDisciplineByIdQuery, GetFixedDisciplineByIdResponse>
{
    private readonly IUnitOfWork<int> _unitOfWork;

    public GetFixedDisciplineByIdQueryHandler(IUnitOfWork<int> unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetFixedDisciplineByIdResponse> Handle(GetFixedDisciplineByIdQuery query, CancellationToken cancellationToken)
    {
        var fixedDiscipline = await _unitOfWork.Repository<FixedDiscipline>().GetByIdAsync(query.Id);
        return fixedDiscipline.Adapt<GetFixedDisciplineByIdResponse>();
    }
}
