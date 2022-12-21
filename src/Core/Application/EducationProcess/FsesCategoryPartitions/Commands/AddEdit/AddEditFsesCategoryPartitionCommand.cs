using System.ComponentModel.DataAnnotations;
using Edu.WebApi.Application.Interfaces.Repositories;
using Edu.WebApi.Domain.EducationProcess;
using Mapster;

namespace Edu.WebApi.Application.EducationProcess.FsesCategoryPartitions.Commands.AddEdit;

public partial class AddEditFsesCategoryPartitionCommand : IRequest<int>
{
    public int Id { get; set; }
    [Required]
    public int FirstPartNumber { get; set; }
    [Required]
    public int SecondPartNumber { get; set; }
    public short? ThirdPathNumber { get; set; }
    [Required]
    public string Name { get; set; } = default!;
    public string? NameAbbreviation { get; set; }
}

internal class AddEditFsesCategoryPartitionCommandHandler : IRequestHandler<AddEditFsesCategoryPartitionCommand, int>
{
    private readonly IStringLocalizer<AddEditFsesCategoryPartitionCommandHandler> _localizer;
    private readonly IUnitOfWork<int> _unitOfWork;

    public AddEditFsesCategoryPartitionCommandHandler(IUnitOfWork<int> unitOfWork, IStringLocalizer<AddEditFsesCategoryPartitionCommandHandler> localizer)
    {
        _unitOfWork = unitOfWork;
        _localizer = localizer;
    }

    public async Task<int> Handle(AddEditFsesCategoryPartitionCommand command, CancellationToken cancellationToken)
    {
        if (command.Id == 0)
        {
            var fsesCategoryPartition = command.Adapt<FsesCategoryPartition>();
            await _unitOfWork.Repository<FsesCategoryPartition>().AddAsync(fsesCategoryPartition);
            await _unitOfWork.Commit(cancellationToken);
            return fsesCategoryPartition.Id;
        }
        else
        {
            var fsesCategoryPartition = await _unitOfWork.Repository<FsesCategoryPartition>().GetByIdAsync(command.Id);

            if (fsesCategoryPartition is null)
            {
                throw new NotFoundException("Fses Category Partition Not Found!");
            }

            fsesCategoryPartition.Name = command.Name;
            await _unitOfWork.Repository<FsesCategoryPartition>().UpdateAsync(fsesCategoryPartition);
            await _unitOfWork.Commit(cancellationToken);
            return fsesCategoryPartition.Id;
        }
    }
}
