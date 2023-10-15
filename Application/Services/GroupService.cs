using Application.IServices;
using Application.Models.Group;
using Newtonsoft.Json;
using RestSharp;

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

        var response = await client.ExecuteAsync<List<GroupResponse>>(request);
        if (response.IsSuccessful)
        {
            return response.Data;
        }
        throw new HttpRequestException("Something went wrong");
    }

    public async Task<GroupResponse> GetGroup(int groupId)
    {
        var client = _restClientService.CreateClient();
        var request = new RestRequest($"groups/{groupId}");

        var response = await client.ExecuteAsync<GroupResponse>(request);

        if (response.IsSuccessful)
        {
            return response.Data;
        }

        throw new Exception("Something went wrong");
    }

    public async Task CreateGroup(CreateGroupRequest createGroupRequest)
    {
        var client = _restClientService.CreateClient();

        var request = new RestRequest($"groups", Method.Post);

        var jsonRequest = JsonConvert.SerializeObject(createGroupRequest);
        request.AddJsonBody(jsonRequest);

        var response = await client.ExecuteAsync(request);
        
        if (!response.IsSuccessful)
        {
            throw new Exception("Something went wrong");
        }
    }
}