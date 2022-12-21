using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;
using Mapster;

namespace Edu.WebApi.Application.EducationProcess.DisciplineSemesters.Queries.GetAll;

public class GetAllDisciplineSemestersQuery : IRequest<List<GetAllDisciplineSemestersResponse>>
{
    public GetAllDisciplineSemestersQuery()
    {
    }
}

internal class GetAllDisciplineSemestersCachedQueryHandler : IRequestHandler<GetAllDisciplineSemestersQuery, List<GetAllDisciplineSemestersResponse>>
{
    private readonly IUnitOfWork<int> _unitOfWork;

    public GetAllDisciplineSemestersCachedQueryHandler(IUnitOfWork<int> unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<GetAllDisciplineSemestersResponse>> Handle(GetAllDisciplineSemestersQuery request, CancellationToken cancellationToken)
    {
        var disciplineSemesters = await _unitOfWork.Repository<DisciplineSemester>().GetAllAsync();
        return disciplineSemesters.Adapt<List<GetAllDisciplineSemestersResponse>>();
    }
}
