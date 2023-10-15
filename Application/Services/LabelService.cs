using Application.IRepositories;
using Application.IServices;
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

    public async Task AddGroupLabels(int groupId, Guid backupId)
    {
        var labels = await GetGroupLabels(groupId);
        var createLabelsRequest = labels.Select(response => new CreateLabelRequest
        {
            LabelName = response.Name,
            Color = response.Color,
            LabelDescription = response.Description,
            BackupId = backupId
        }).ToList();

        await _labelRepository.CreateLabels(createLabelsRequest);
    }

    public async Task RestoreGroupLabels(Guid backupId, int groupId)
    {
        var labels = await _labelRepository.GetBackupLabels(backupId);

        var restoreLabelsRequest = labels.Select(l => new RestoreLabelRequest
        {
            GroupId = groupId,
            LabelName = l.LabelName,
            Color = l.Color,
            LabelDescription = l.LabelDescription
        }).ToList();

        var client = _restClientService.CreateClient();

        var request = new RestRequest($"groups/{groupId}/labels", Method.Post);

        foreach (var restoreLabelRequest in restoreLabelsRequest.Select(JsonConvert.SerializeObject))
        {
            request.AddJsonBody(restoreLabelRequest);

            var response = await client.ExecuteAsync(request);
            if (!response.IsSuccessful)
            {
                throw new Exception("Something went wrong");
            }
        }
    }
    
    private async Task<List<LabelResponse>> GetGroupLabels(int groupId)
    {
        var client = _restClientService.CreateClient();

        var request = new RestRequest($"groups/{groupId}/labels");

        var response = await client.ExecuteAsync(request);

        if (response.IsSuccessful)
        {
            return JsonConvert.DeserializeObject<List<LabelResponse>>(response.Content);
        }

        throw new Exception("Something went wrong");
    }
}