using Application.IServices;
using Microsoft.Extensions.Configuration;
using RestSharp;

namespace Application.Services;

public class RestClientService : IRestClientService
{
    private readonly IConfiguration _configuration;

    public RestClientService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public RestClient CreateClient()
    {
        var rootPath = _configuration.GetSection("GitLab:GitLabRootPath").Value;
        var pat = _configuration.GetSection("GitLab:PersonalAccessToken").Value;

        var client = new RestClient(rootPath);
        client.AddDefaultParameter("private_token", pat);

        return client;
    }
}