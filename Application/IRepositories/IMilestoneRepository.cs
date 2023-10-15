using Application.Models.Milestone;

namespace Application.IRepositories;

public interface IMilestoneRepository
{
    public Task CreateMilestones(List<CreateMilestonesRequest> request);
}