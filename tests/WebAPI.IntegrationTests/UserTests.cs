using System.Net.Http.Json;
using WebAPI.EndPoints;

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
    public async Task GetUserById_ReturnsAUser()
    {
        int userId = 1;
        //Skicka en HTTP Request till /api/users/1
        var response = await _client.GetFromJsonAsync<GetUserByID.Response>($"api/users/{userId}");

        //Kolla att svaret innehåller det vi förväntar oss
        Assert.NotNull(response);
        Assert.Equal(userId, response?.Id);
        Assert.False(string.IsNullOrEmpty(response?.Name));
    }

    [Fact]
    public async Task GetUserById_ReturnsNotFound()
    {
        var response = await _client.GetAsync("api/users/0");

        Assert.Equal(404, (int)response.StatusCode);
    }

    [Fact]
    public async Task GetAllUsers_ReturnsAllUsers()
    {
        var response = await _client.GetFromJsonAsync<List<UserResponse>>("api/users");

        Assert.NotNull(response);
        Assert.True(response?.Count > 0);
    }

    public record UserResponse(int Id, string Name);
}