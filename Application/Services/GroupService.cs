using Application.Group;
using Application.Models.Backup;
using Application.Models.Group;
using Domain;
using Newtonsoft.Json;
using RestSharp;
using Microsoft.Extensions.Configuration;

namespace Application.Repository;

public class GroupService : IGroupService
{
    private readonly IBackupRepository _backupRepository;
    private readonly IConfiguration _configuration;

    public GroupService(IBackupRepository backupRepository, IConfiguration configuration)
    {
        _backupRepository = backupRepository;
        _configuration = configuration;
    }

    public List<GroupResponse> GetAllGroups()
    {
        var rootPath = _configuration.GetSection("GitLab:GitLabRootPath").Value;
        var PAT = _configuration.GetSection("GitLab:PersonalAccessToken").Value;

        var client = new RestClient(rootPath);
        client.AddDefaultParameter("private_token", PAT);

        var request = new RestRequest("groups", Method.Get);

        var response = client.Execute(request);

        if (response.IsSuccessful)
        {
            return JsonConvert.DeserializeObject<List<GroupResponse>>(response.Content!);
        }
        else
        {
            Console.WriteLine($"Request failed with status code {response.StatusCode}");
            Console.WriteLine($"Error message: {response.ErrorMessage}");
        }

        return null;
    }

    public int CreateGroup(int groupId)
    {
        var rootPath = _configuration.GetSection("GitLab:GitLabRootPath").Value;
        var PAT = _configuration.GetSection("GitLab:PersonalAccessToken").Value;

        var client = new RestClient(rootPath);
        client.AddDefaultParameter("private_token", PAT);

        var backup = _backupRepository.GetLatestBackup(groupId);
        var group = new CreateGroupRequest
        {
            Name = backup.Name,
            Path = backup.Path,
            Visibility = backup.Visibility,
            Description = backup.Description
        };

        var request = new RestRequest($"groups", Method.Post);
        request.AddJsonBody(group);
        
        var response = client.Execute(request);
        return backup.Id;
    }

    public Backup CreateBackup(int groupId, bool isSimple)
    {
        var rootPath = _configuration.GetSection("GitLab:GitLabRootPath").Value;
        var PAT = _configuration.GetSection("GitLab:PersonalAccessToken").Value;

        var client = new RestClient(rootPath);
        client.AddDefaultParameter("private_token", PAT);

        var request = new RestRequest($"groups/{groupId}", Method.Get);

        var response = client.Execute(request);

        var data = JsonConvert.DeserializeObject<GroupResponse>(response.Content!);

        var backup = new Backup
        {
            Id = data.Id,
            Name = data.Name,
            Path = data.Path,
            Visibility = "private",
            Description = "123",
            CreatedAt = DateTime.Now
        };

        return _backupRepository.CreateBackup(backup);
    }
}