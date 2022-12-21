using System.ComponentModel.DataAnnotations;
using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;
using Mapster;

namespace Edu.WebApi.Application.EducationProcess.DisciplineSemesters.Commands.AddEdit;

public partial class AddEditDisciplineSemesterCommand : IRequest<int>
{
    public int Id { get; set; }
    [Required]
    public byte SemesterNumber { get; set; }
    public byte? WeeksCount { get; set; }
    [Required]
    public int DisciplineId { get; set; }
    [Required]
    public short TheoryLessonHours { get; set; }
    [Required]
    public short PracticeWorkHours { get; set; }
    [Required]
    public short LaboratoryWorkHours { get; set; }
    [Required]
    public short ControlWorkHours { get; set; }
    [Required]
    public short IndependentWorkHours { get; set; }
    [Required]
    public short ConsultationHours { get; set; }
    [Required]
    public short ExamHours { get; set; }
    [Required]
    public short EducationalPracticeHours { get; set; }
    [Required]
    public short ProductionPracticeHours { get; set; }
    public int? CertificationFormId { get; set; }
    public string? Description { get; set; }
}

internal class AddEditDisciplineSemesterCommandHandler : IRequestHandler<AddEditDisciplineSemesterCommand, int>
{
    private readonly IStringLocalizer<AddEditDisciplineSemesterCommandHandler> _localizer;
    private readonly IUnitOfWork<int> _unitOfWork;

    public AddEditDisciplineSemesterCommandHandler(IUnitOfWork<int> unitOfWork, IStringLocalizer<AddEditDisciplineSemesterCommandHandler> localizer)
    {
        _unitOfWork = unitOfWork;
        _localizer = localizer;
    }

    public async Task<int> Handle(AddEditDisciplineSemesterCommand command, CancellationToken cancellationToken)
    {
        if (command.Id == 0)
        {
            var disciplineSemester = command.Adapt<DisciplineSemester>();
            await _unitOfWork.Repository<DisciplineSemester>().AddAsync(disciplineSemester);
            await _unitOfWork.Commit(cancellationToken);
            return disciplineSemester.Id;
        }
        else
        {
            var disciplineSemester = await _unitOfWork.Repository<DisciplineSemester>().GetByIdAsync(command.Id);

            if (disciplineSemester is null)
            {
                throw new NotFoundException("Discipline Semester Not Found!");
            }

            disciplineSemester.SemesterNumber = command.SemesterNumber;
            disciplineSemester.WeeksCount = command.WeeksCount;
            disciplineSemester.DisciplineId = command.DisciplineId;
            disciplineSemester.TheoryLessonHours = command.TheoryLessonHours;
            disciplineSemester.PracticeWorkHours = command.PracticeWorkHours;
            disciplineSemester.LaboratoryWorkHours = command.LaboratoryWorkHours;
            disciplineSemester.ControlWorkHours = command.ControlWorkHours;
            disciplineSemester.IndependentWorkHours = command.IndependentWorkHours;
            disciplineSemester.ConsultationHours = command.ConsultationHours;
            disciplineSemester.ExamHours = command.ExamHours;
            disciplineSemester.EducationalPracticeHours = command.EducationalPracticeHours;
            disciplineSemester.ProductionPracticeHours = command.ProductionPracticeHours;
            disciplineSemester.CertificationFormId = command.CertificationFormId;
            disciplineSemester.Description = command.Description;

            await _unitOfWork.Repository<DisciplineSemester>().UpdateAsync(disciplineSemester);
            await _unitOfWork.Commit(cancellationToken);
            return disciplineSemester.Id;
        }
    }
}
