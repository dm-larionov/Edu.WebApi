using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;
using Mapster;

namespace Edu.WebApi.Application.EducationProcess.Disciplines.Queries.GetById;

public class GetDisciplineByIdQuery : IRequest<GetDisciplineByIdResponse>
{
    public int Id { get; set; }
}

internal class GetDisciplineByIdQueryHandler : IRequestHandler<GetDisciplineByIdQuery, GetDisciplineByIdResponse>
{
    private readonly IUnitOfWork<int> _unitOfWork;

    public GetDisciplineByIdQueryHandler(IUnitOfWork<int> unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetDisciplineByIdResponse> Handle(GetDisciplineByIdQuery query, CancellationToken cancellationToken)
    {
        var discipline = await _unitOfWork.Repository<Discipline>().GetByIdAsync(query.Id);
        return discipline.Adapt<GetDisciplineByIdResponse>();
    }
}
