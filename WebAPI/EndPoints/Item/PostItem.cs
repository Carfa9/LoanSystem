using WebAPI.Infrastructure;
using WebAPI.Models;

namespace WebAPI.EndPoints;

public static class PostItem
{
    public record Request(string Name, string Description, int OwnerId);

    public class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapPost("api/items", Handler);

        public static async Task<IResult> Handler(Request request, LoanDbContext db)
        {
            var item = new Item(request.Name, request.Description, request.OwnerId);

            db.Items.Add(item);
            await db.SaveChangesAsync();

            return Results.Created($"/api/items/{item.Id}", new { item.Id, item.Name, item.Description, item.OwnerId });
        }
    }
}