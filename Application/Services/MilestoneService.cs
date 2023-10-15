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
    
    public async Task<List<MilestoneResponse>> GetGroupMilestones(int groupId)
    {
        var client = _restClientService.CreateClient();

        var request = new RestRequest($"groups/{groupId}/milestones");

        var response = await client.ExecuteAsync(request);

        if (response.IsSuccessful)
        {
            return JsonConvert.DeserializeObject<List<MilestoneResponse>>(response.Content);
        }
        throw new Exception();
    }
    
    public async Task AddGroupMilestones(int groupId, Guid backupId)
    {
        var milestones = await GetGroupMilestones(groupId);
        var createMilestonesRequest = milestones.Select(response => new CreateMilestonesRequest
        {
            GroupId = response.GroupId,
            MilestoneTitle = response.MilestoneTitle,
            DueDate = response.DueDate,
            StartDate = response.StartDate,
            MilestoneDescription = response.MilestoneDescription,
            BackupId = backupId
        }).ToList();

        await _milestoneRepository.CreateMilestones(createMilestonesRequest);
    }

    public async Task CreateMilestones()
    {
        
    }
}