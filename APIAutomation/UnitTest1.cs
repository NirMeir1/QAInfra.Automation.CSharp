using System.Net;

using NUnit.Framework;

namespace APIAutomation;

public class Tests : ApiTestBase
{
    [Test]
    public async Task GetPostReturnsId1()
    {
        var response = await GetAsync<Post>("/posts/1");
        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(response.Data?.Id, Is.EqualTo(1));
        });
    }

    private class Post
    {
        public int Id { get; set; }
    }
}
