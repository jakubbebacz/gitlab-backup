using Application.Models.Milestone;
using Domain;

namespace Application.IRepositories;

public interface IMilestoneRepository
{
    public Task<List<Milestone>> GetBackupMilestones(Guid backupId);

    public Task CreateMilestones(List<CreateMilestoneRequest> request);
}