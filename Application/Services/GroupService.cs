using Application.IServices;
using Application.Models.Group;
using Newtonsoft.Json;
using RestSharp;
using System.Text.Json;
using Application.Models.Backup;

namespace Application.Services;

public class GroupService : IGroupService
{
    private readonly IRestClientService _restClientService;

    public GroupService(IRestClientService restClientService)
    {
        _restClientService = restClientService;
    }

    public async Task<List<GroupResponse>> GetAllGroups()
    {
        var client = _restClientService.CreateClient();

        var request = new RestRequest("groups");

        var response = await client.ExecuteAsync(request);

        if (response.IsSuccessful)
        {
            return JsonConvert.DeserializeObject<List<GroupResponse>>(response.Content!);
        }
        throw new Exception();
    }

    public async Task<GroupResponse> GetGroup(int groupId)
    {
        var client = _restClientService.CreateClient();
        var request = new RestRequest($"groups/{groupId}");

        var response = await client.ExecuteAsync(request);

        return JsonConvert.DeserializeObject<GroupResponse>(response.Content!);
    }

    public async Task CreateGroup(CreateGroupRequest createGroupRequest)
    {
        var client = _restClientService.CreateClient();
        
        var request = new RestRequest($"groups", Method.Post);
        
        var jsonRequest = JsonConvert.SerializeObject(createGroupRequest);
        request.AddJsonBody(jsonRequest);

        await client.ExecuteAsync(request);
    }
}