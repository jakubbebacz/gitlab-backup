using Application.Models.Backup;
using Application.Models.Label;

namespace Application.IServices;

public interface ILabelService
{
    public Task<List<LabelResponse>> GetGroupLabels(int groupId);

    public Task AddGroupLabels(int groupId, Guid backupId);
}