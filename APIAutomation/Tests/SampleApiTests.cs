using System.Net;
using APIAutomation.Infra;
using APIAutomation.models;
using NUnit.Framework;
using RestSharp;

namespace APIAutomation.Tests;

/// <summary>
/// Sample API tests demonstrating parameterization and custom request setup.
/// </summary>
public class SampleApiTests : ApiTestBase
{
    [TestCase(1)]
    [TestCase(2)]
    public async Task GetPostReturnsOk(int id)
    {
        var response = await GetAsync<Post>($"/posts/{id}");
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.That(response.Data?.Id, Is.EqualTo(id));
    }
}

/// <summary>
/// Demonstrates adding custom headers for each request.
/// </summary>
public class CustomHeaderTests : ApiTestBase
{
    protected override void CustomizeRequest(RestRequest request)
    {
        request.AddHeader("X-Test", "demo");
    }

    [Test]
    public async Task RequestWithCustomHeader()
    {
        var response = await GetAsync<Post>("/posts/1");
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }
}
