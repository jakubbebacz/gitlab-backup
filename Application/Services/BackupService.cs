using Application.IRepositories;
using Application.IServices;
using Application.Models.Backup;
using Application.Models.Group;

namespace Application.Services;

public class BackupService : IBackupService
{
    private readonly IGroupService _groupService;
    private readonly IBackupRepository _backupRepository;
    private readonly ILabelService _labelService;
    private readonly IMilestoneService _milestoneService;

    public BackupService(
        IGroupService groupService,
        IBackupRepository backupRepository,
        ILabelService labelService,
        IMilestoneService milestoneService)
    {
        _groupService = groupService;
        _backupRepository = backupRepository;
        _labelService = labelService;
        _milestoneService = milestoneService;
    }

    public async Task<List<BackupResponse>> GetAllBackups()
    {
        var backups = await _backupRepository.GetAllBackups();
        var response = backups.Select(b => new BackupResponse
        {
            BackupId = b.BackupId,
            GroupId = b.GroupId,
            BackupName = b.BackupName,
            BackupPath = b.BackupPath,
            Visibility = b.Visibility!,
            BackupDescription = b.BackupDescription ?? string.Empty
        }).ToList();

        return response;
    }

    public async Task<Guid> CreateBackup(int groupId, bool isSimple)
    {
        try
        {
            var group = await _groupService.GetGroup(groupId);

            var createBackupRequest = new CreateBackupRequest
            {
                GroupId = group.Id,
                BackupName = group.Name,
                BackupPath = group.Path,
                Visibility = group.Visibility,
                BackupDescription = group.Description,
                CreatedAt = DateTime.Now
            };

            var backupId = await _backupRepository.CreateBackup(createBackupRequest);

            if (isSimple) return backupId;

            await _labelService.AddGroupLabels(groupId, backupId);
            await _milestoneService.AddGroupMilestones(groupId, backupId);

            return backupId;
        }
        catch (Exception)
        {
            throw new Exception("Something went wrong");
        }
    }

    public async Task<Guid> RestoreBackup(int groupId)
    {
        try
        {
            var backup = await _backupRepository.GetLatestBackup(groupId);
            var groupRequest = new CreateGroupRequest
            {
                Name = backup.BackupName,
                Path = backup.BackupPath,
                Visibility = backup.Visibility!,
                Description = backup.BackupDescription ?? string.Empty
            };

            await _groupService.CreateGroup(groupRequest);

            if (backup.Labels.Any())
            {
               await _labelService.RestoreGroupLabels(backup.BackupId, groupId);
            }

            if (backup.Milestones.Any())
            {
                await _milestoneService.RestoreGroupMilestones(backup.BackupId, groupId);
            }

            return backup.BackupId;
        }
        catch (Exception)
        {
            throw new Exception("Something went wrong");
        }
    }
}