using Application.IRepositories;
using Application.Models.Milestone;
using Domain;

namespace Infrastructure.Repositories;

public class MilestoneRepository : IMilestoneRepository
{
    private readonly GitLabDbContext _gitLabDbContext;

    public MilestoneRepository(GitLabDbContext gitLabDbContext)
    {
        _gitLabDbContext = gitLabDbContext;
    }
    
    public async Task CreateMilestones(List<CreateMilestonesRequest> request)
    {
        foreach (var milestone in request.Select(createMilestonesRequest => new Milestone
                 {
                     MilestoneId = new Guid(),
                     BackupId = createMilestonesRequest.BackupId,
                     MilestoneTitle = createMilestonesRequest.MilestoneTitle,
                     DueDate = createMilestonesRequest.DueDate,
                     StartDate = createMilestonesRequest.StartDate,
                     MilestoneDescription = createMilestonesRequest.MilestoneDescription
                 }))
        {
            _gitLabDbContext.Milestones.Add(milestone);
        }

        await _gitLabDbContext.SaveChangesAsync();
    }
}