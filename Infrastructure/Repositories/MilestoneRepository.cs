using Application.IRepositories;
using Application.Models.Milestone;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class MilestoneRepository : IMilestoneRepository
{
    private readonly GitLabDbContext _gitLabDbContext;

    public MilestoneRepository(GitLabDbContext gitLabDbContext)
    {
        _gitLabDbContext = gitLabDbContext;
    }

    public async Task<List<Milestone>> GetBackupMilestones(Guid backupId)
    {
        return await _gitLabDbContext.Milestones.ToListAsync();
    }

    public async Task CreateMilestones(List<CreateMilestoneRequest> request)
    {
        request.Select(createMilestonesRequest => new Milestone
        {
            MilestoneId = new Guid(),
            BackupId = createMilestonesRequest.BackupId,
            MilestoneTitle = createMilestonesRequest.MilestoneTitle,
            DueDate = createMilestonesRequest.DueDate,
            StartDate = createMilestonesRequest.StartDate,
            MilestoneDescription = createMilestonesRequest.MilestoneDescription
        })
            .ToList()
            .ForEach(m => _gitLabDbContext.Milestones.Add(m));

        await _gitLabDbContext.SaveChangesAsync();
    }
}