using Application.IRepositories;
using Application.Models.Label;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class LabelRepository : ILabelRepository
{
    private readonly GitLabDbContext _gitLabDbContext;

    public LabelRepository(GitLabDbContext gitLabDbContext)
    {
        _gitLabDbContext = gitLabDbContext;
    }

    public async Task<List<Label>> GetBackupLabels(Guid backupId)
    {
        return await _gitLabDbContext.Labels.ToListAsync();
    }

    public async Task CreateLabels(List<CreateLabelRequest> request)
    {
        foreach (var label in request.Select(createLabelRequest => new Label
                 {
                     LabelId = new Guid(),
                     BackupId = createLabelRequest.BackupId,
                     LabelName = createLabelRequest.LabelName,
                     Color = createLabelRequest.Color,
                     LabelDescription = createLabelRequest.LabelDescription
                 }))
        {
            _gitLabDbContext.Labels.Add(label);
        }

        await _gitLabDbContext.SaveChangesAsync();
    }
}