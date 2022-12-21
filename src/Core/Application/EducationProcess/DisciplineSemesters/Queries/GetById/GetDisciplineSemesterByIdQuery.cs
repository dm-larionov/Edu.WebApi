using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;
using Mapster;

namespace Edu.WebApi.Application.EducationProcess.DisciplineSemesters.Queries.GetById;

public class GetDisciplineSemesterByIdQuery : IRequest<GetDisciplineSemesterByIdResponse>
{
    public int Id { get; set; }
}

internal class GetDisciplineSemesterByIdQueryHandler : IRequestHandler<GetDisciplineSemesterByIdQuery, GetDisciplineSemesterByIdResponse>
{
    private readonly IUnitOfWork<int> _unitOfWork;

    public GetDisciplineSemesterByIdQueryHandler(IUnitOfWork<int> unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetDisciplineSemesterByIdResponse> Handle(GetDisciplineSemesterByIdQuery query, CancellationToken cancellationToken)
    {
        var disciplineSemester = await _unitOfWork.Repository<DisciplineSemester>().GetByIdAsync(query.Id);
        return disciplineSemester.Adapt<GetDisciplineSemesterByIdResponse>();
    }
}
