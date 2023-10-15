using RestSharp;

namespace Application.IServices;

public interface IRestClientService
{
    public RestClient CreateClient();
}