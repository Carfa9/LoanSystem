using System.Net.Http.Json;
using System.Text.Json;
using WebAPI.EndPoints;
using WebAPI.Models;

namespace WebAPI.IntegrationTests;

public class ItemTests
{
    [Fact]
    public async Task PostItemRequest_AddsItem()
    {
        //Arrange
        LoanSystemWebAppFactory factory = new();
        HttpClient client = factory.CreateClient();

        PostItem.Request request = new("Rake", "A tool for gardening", 1);

        //Act
        var response = await client.PostAsJsonAsync("api/items", request);

        //Assert
        response.EnsureSuccessStatusCode();

        //deserialisera svaret från API:t

        var responseContent = await response.Content.ReadAsStringAsync();
        var user = JsonSerializer.Deserialize<User>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }

    [Fact]
    public async Task ShouldGetAllItemsAndItsOwner()
    {
        //Arrange
        LoanSystemWebAppFactory factory = new();
        HttpClient client = factory.CreateClient();

        //Act
        var response = await client.GetAsync("api/items");

        //Assert
        response.EnsureSuccessStatusCode();

        //deserialisera svaret från API:t
        var responseContent = await response.Content.ReadAsStringAsync();
        var items = JsonSerializer.Deserialize<List<Item>>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

    }
}