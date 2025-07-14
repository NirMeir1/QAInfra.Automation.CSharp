using Common.Utils;
using NUnit.Framework;
using RestSharp;

namespace APIAutomation.Infra;

/// <summary>
/// Base class for API tests providing a RestSharp client and simple logging.
/// Allows customization of client and requests.
/// </summary>
[Parallelizable(ParallelScope.All)]
public abstract class ApiTestBase
{
    protected RestClient Client { get; private set; } = default!;
    protected TestLogger Log = default!;

    [OneTimeSetUp]
    public void GlobalSetUp()
    {
        var options = new RestClientOptions("https://jsonplaceholder.typicode.com");
        CustomizeClientOptions(options);
        Client = new RestClient(options);
        CustomizeClient(Client);
    }

    /// <summary>
    /// Override to adjust RestClientOptions (e.g., base URL, auth).
    /// </summary>
    protected virtual void CustomizeClientOptions(RestClientOptions options) { }

    /// <summary>
    /// Override to adjust the RestClient instance.
    /// </summary>
    protected virtual void CustomizeClient(RestClient client) { }

    /// <summary>
    /// Override to customize an individual request (headers, auth, etc.).
    /// </summary>
    protected virtual void CustomizeRequest(RestRequest request) { }

    protected async Task<RestResponse<T>> GetAsync<T>(string resource)
    {
        var request = new RestRequest(resource, Method.Get);
        CustomizeRequest(request);
        var response = await Client.ExecuteAsync<T>(request);
        if (!response.IsSuccessful)
        {
            TestLogger.Error($"Request to {resource} failed with {response.StatusCode}");
            throw new AssertionException($"Request failed: {response.StatusCode}");
        }
        return response;
    }
}
