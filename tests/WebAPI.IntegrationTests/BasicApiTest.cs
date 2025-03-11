using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Testing;
using WebAPI;

namespace WebAPI.IntegrationTests;

public class basicApiTest
{
    [Fact]
    public async Task RootShouldReturnHelloWorld()
    {
        WebApplicationFactory<Program> factory = new();
        HttpClient client = factory.CreateClient();

        var response = await client.GetAsync("/");

        Assert.Equal("Hello World!", response.Content.ReadAsStringAsync().Result);
    }
}

