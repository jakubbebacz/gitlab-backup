using System.Transactions;
using Application.IRepositories;
using Application.IServices;
using Application.Models.Backup;
using Application.Models.Group;
using Application.Models.Label;
using Domain;
using RestSharp;

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
            BackupDescription = b.BackupDescription ?? "",
            Visibility = b.Visibility ?? "private"
        }).ToList();

        return response;
    }

    public async Task<Guid> CreateBackup(int groupId, bool isSimple)
    {
        using var transaction = new TransactionScope();
        try
        {
            var group = await _groupService.GetGroup(groupId);

            var createBackupRequest = new CreateBackupRequest
            {
                GroupId = group.GroupId,
                BackupName = group.GroupName,
                BackupPath = group.GroupPath,
                Visibility = group.Visibility,
                BackupDescription = group.GroupDescription,
                CreatedAt = DateTime.Now
            };

            var backupId = await _backupRepository.CreateBackup(createBackupRequest);

            if (!isSimple)
            {
                await _labelService.AddGroupLabels(groupId, backupId);
                await _milestoneService.AddGroupMilestones(groupId, backupId);
            }

            transaction.Complete();
            return backupId;
        }
        catch (Exception)
        {
            transaction.Dispose();
            throw;
        }
    }

    public async Task<Guid> RestoreBackup(int groupId)
    {
        using var transaction = new TransactionScope();
        try
        {
            var backup = await _backupRepository.GetLatestBackup(groupId);
            var groupRequest = new CreateGroupRequest
            {
                Name = backup.BackupName,
                Path = backup.BackupPath,
                Visibility = backup.Visibility ?? "",
                Description = backup.BackupDescription ?? "private"
            };

            await _groupService.CreateGroup(groupRequest);
            transaction.Complete();
            return backup.BackupId;
        }
        catch (Exception)
        {
            transaction.Dispose();
            throw;
        }
    }
}