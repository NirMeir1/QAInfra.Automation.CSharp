using RestSharp;

namespace APIAutomation;

public class Tests
{
    [Test]
    public async Task GetPostReturnsId1()
    {
        var client = new RestClient("https://jsonplaceholder.typicode.com");
        var request = new RestRequest("/posts/1", Method.Get);
        var response = await client.ExecuteGetAsync<Post>(request);
        Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
        Assert.That(response.Data?.Id, Is.EqualTo(1));
    }

    private class Post
    {
        public int Id { get; set; }
    }
}
