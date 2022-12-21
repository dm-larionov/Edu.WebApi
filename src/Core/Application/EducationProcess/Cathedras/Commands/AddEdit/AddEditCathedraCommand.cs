using System.ComponentModel.DataAnnotations;
using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;
using Mapster;

namespace Edu.WebApi.Application.EducationProcess.Cathedras.Commands.AddEdit;

public partial class AddEditCathedraCommand : IRequest<int>
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = default!;
    public string NameAbbreviation { get; set; } = default!;
    public string Description { get; set; } = default!;
}

internal class AddEditCathedraCommandHandler : IRequestHandler<AddEditCathedraCommand, int>
{
    private readonly IStringLocalizer<AddEditCathedraCommandHandler> _localizer;
    private readonly IUnitOfWork<int> _unitOfWork;

    public AddEditCathedraCommandHandler(IUnitOfWork<int> unitOfWork, IStringLocalizer<AddEditCathedraCommandHandler> localizer)
    {
        _unitOfWork = unitOfWork;
        _localizer = localizer;
    }

    public async Task<int> Handle(AddEditCathedraCommand command, CancellationToken cancellationToken)
    {
        if (command.Id == 0)
        {
            var cathedra = command.Adapt<Cathedra>();
            await _unitOfWork.Repository<Cathedra>().AddAsync(cathedra);
            await _unitOfWork.Commit(cancellationToken);
            return cathedra.Id;
        }
        else
        {
            var cathedra = await _unitOfWork.Repository<Cathedra>().GetByIdAsync(command.Id);

            if (cathedra is null)
            {
                throw new NotFoundException("Cathedra Not Found!");
            }

            cathedra.Name = command.Name;
            await _unitOfWork.Repository<Cathedra>().UpdateAsync(cathedra);
            await _unitOfWork.Commit(cancellationToken);
            return cathedra.Id;
        }
    }
}
