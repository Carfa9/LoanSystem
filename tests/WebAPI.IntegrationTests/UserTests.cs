using System.Net.Http.Json;
using WebAPI.EndPoints;
using WebAPI.Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace WebAPI.IntegrationTests;

public class UserTests : IClassFixture<WebAppFactoryFixture>
{
    private readonly HttpClient _client;

    public UserTests(WebAppFactoryFixture fixture)
    {
        _client = fixture.Factory.CreateClient();
    }

    [Fact]
    public async Task PostUserRequest_AddsUser()
    {
        //Arrange
        PostUser.Request request = new("John");

        //Act
        var response = await _client.PostAsJsonAsync("api/users", request);

        //Assert
        response.EnsureSuccessStatusCode();

        //Deserialisera svaret från responsebodyn från json till ett PostUser.Response
        PostUser.Response? content = await response.Content.ReadFromJsonAsync<PostUser.Response>();
        Assert.Equal("John", content?.Name);
        Assert.True(content?.Id > 0);
    }

        [Fact]
    public async Task ShouldGetAllUsers()
    {
        //Arrange
        LoanSystemWebAppFactory factory = new();
        HttpClient client = factory.CreateClient();

        //Act
        var response = await client.GetAsync("api/users");

        //Assert
        response.EnsureSuccessStatusCode();

        //deserialisera svaret från API:t

        var responseContent = await response.Content.ReadAsStringAsync();
        var users = JsonSerializer.Deserialize<List<User>>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }

    [Fact]
    public async Task ShouldGetUserById()
    {
        //Arrange
        LoanSystemWebAppFactory factory = new();
        HttpClient client = factory.CreateClient();

        //Act
        var response = await client.GetAsync("api/users/1");

        //Assert
        response.EnsureSuccessStatusCode();

        //deserialisera svaret från API:t
        var responseContent = await response.Content.ReadAsStringAsync();
        var user = JsonSerializer.Deserialize<User>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        



    }
}
