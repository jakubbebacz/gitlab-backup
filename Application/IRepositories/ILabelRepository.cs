using Application.Models.Label;
using Domain;

namespace Application.IRepositories;

public interface ILabelRepository
{
    public Task<List<Label>> GetBackupLabels(Guid backupId);

    public Task CreateLabels(List<CreateLabelRequest> request);
}