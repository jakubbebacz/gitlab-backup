using Application.IRepositories;
using Application.IServices;
using Application.Models.Backup;
using Application.Models.Label;
using Newtonsoft.Json;
using RestSharp;

namespace Application.Services;

public class LabelService : ILabelService
{
    private readonly IRestClientService _restClientService;
    private readonly ILabelRepository _labelRepository;

    public LabelService(IRestClientService restClientService, ILabelRepository labelRepository)
    {
        _restClientService = restClientService;
        _labelRepository = labelRepository;
    }
    
    public async Task<List<LabelResponse>> GetGroupLabels(int groupId)
    {
        var client = _restClientService.CreateClient();

        var request = new RestRequest($"groups/{groupId}/labels");

        var response = await client.ExecuteAsync(request);

        if (response.IsSuccessful)
        {
            return JsonConvert.DeserializeObject<List<LabelResponse>>(response.Content);
        }
        throw new Exception();
    }

    public async Task AddGroupLabels(int groupId, Guid backupId)
    {
        var labels = await GetGroupLabels(groupId);
        var createLabelsRequest = labels.Select(response => new CreateLabelsRequest
        {
            GroupId = response.GroupId,
            LabelName = response.LabelName,
            Color = response.Color,
            LabelDescription = response.LabelDescription,
            BackupId = backupId
        }).ToList();

        await _labelRepository.CreateLabels(createLabelsRequest);
    }

}