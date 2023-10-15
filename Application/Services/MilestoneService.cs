using Application.IRepositories;
using Application.IServices;
using Application.Models.Milestone;
using Newtonsoft.Json;
using RestSharp;

namespace Application.Services;

public class MilestoneService : IMilestoneService
{
    private readonly IRestClientService _restClientService;
    private readonly IMilestoneRepository _milestoneRepository;

    public MilestoneService(IRestClientService restClientService, IMilestoneRepository milestoneRepository)
    {
        _restClientService = restClientService;
        _milestoneRepository = milestoneRepository;
    }

    public async Task AddGroupMilestones(int groupId, Guid backupId)
    {
        var milestones = await GetGroupMilestones(groupId);
        var createMilestonesRequest = milestones.Select(response => new CreateMilestoneRequest
        {
            MilestoneTitle = response.Title,
            DueDate = response.DueDate,
            StartDate = response.StartDate,
            MilestoneDescription = response.Description,
            BackupId = backupId
        }).ToList();

        await _milestoneRepository.CreateMilestones(createMilestonesRequest);
    }

    public async Task RestoreGroupMilestones(Guid backupId, int groupId)
    {
        var labels = await _milestoneRepository.GetBackupMilestones(backupId);

        var restoreMilestonesRequest = labels.Select(m => new RestoreMilestoneRequest
        {
            GroupId = groupId,
            MilestoneTitle = m.MilestoneTitle,
            DueDate = m.DueDate,
            StartDate = m.StartDate,
            MilestoneDescription = m.MilestoneDescription
        }).ToList();

        var client = _restClientService.CreateClient();

        var request = new RestRequest($"groups/{groupId}/milestones", Method.Post);

        foreach (var restoreMilestoneRequest in restoreMilestonesRequest.Select(JsonConvert.SerializeObject))
        {
            request.AddJsonBody(restoreMilestoneRequest);

            var response = await client.ExecuteAsync(request);
            if (!response.IsSuccessful)
            {
                throw new Exception("Something went wrong");
            }
        }
    }

    private async Task<List<MilestoneResponse>> GetGroupMilestones(int groupId)
    {
        var client = _restClientService.CreateClient();

        var request = new RestRequest($"groups/{groupId}/milestones");

        var response = await client.ExecuteAsync(request);

        if (response.IsSuccessful)
        {
            return JsonConvert.DeserializeObject<List<MilestoneResponse>>(response.Content);
        }

        throw new Exception("Something went wrong");
    }
}