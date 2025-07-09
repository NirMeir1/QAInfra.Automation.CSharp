using RestSharp;
using NUnit.Framework;

namespace APIAutomation;

public abstract class ApiTestBase
{
    protected RestClient Client { get; private set; } = default!;

    [OneTimeSetUp]
    public void GlobalSetUp()
    {
        Client = new RestClient("https://jsonplaceholder.typicode.com");
    }

    protected async Task<RestResponse<T>> GetAsync<T>(string resource)
    {
        var request = new RestRequest(resource, Method.Get);
        return await Client.ExecuteGetAsync<T>(request);
    }
}
