using Application.Models.Label;

namespace Application.IServices;

public interface ILabelService
{
    public Task AddGroupLabels(int groupId, Guid backupId);

    public Task RestoreGroupLabels(Guid backupId, int groupId);
}