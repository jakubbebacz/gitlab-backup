using Application.Models.Milestone;

namespace Application.IServices;

public interface IMilestoneService
{
    public Task<List<MilestoneResponse>> GetGroupMilestones(int groupId);

    public Task AddGroupMilestones(int groupId, Guid backupId);
}