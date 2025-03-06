using WebAPI.Infrastructure;
using WebAPI.Models;

namespace WebAPI.EndPoints;

public static class PostUser
{
    public record Request(string Name);
    public record Response(int Id, string Name);

    public class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapPost("api/users", Handler);

        public static async Task<IResult> Handler(Request request, LoanDbContext db)
        {
            var user = new User { Name = request.Name };
            db.Users.Add(user);
            await db.SaveChangesAsync();

            return Results.Created("api/users", new Response(user.Id, user.Name));
        }
    }
}