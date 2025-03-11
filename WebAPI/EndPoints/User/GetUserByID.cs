using WebAPI.Infrastructure;
using WebAPI.Models;

namespace WebAPI.EndPoints;

public static class GetUserByID
{
    public record Response(int Id, string Name);

    public class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapGet("api/users/{id:int}", Handler);

        public static IResult Handler(int id, LoanDbContext db)
        {
            User? user = db.Users.Find(id);

            if (user is null)
            {
                return Results.NotFound();
            }

            return Results.Ok(new Response(user.Id, user.Name));
        }
    }
}