using Application.Models.Milestone;

namespace Application.IServices;

public interface IMilestoneService
{
    public Task AddGroupMilestones(int groupId, Guid backupId);

    public Task RestoreGroupMilestones(Guid backupId, int groupId);
}