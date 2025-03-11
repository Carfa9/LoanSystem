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

<<<<<<< HEAD
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
=======
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
>>>>>>> 1088924d5f2b2cee3e1b13e6ae4a97def60fba77
