using Microsoft.EntityFrameworkCore;
using WebAPI.Infrastructure;

namespace WebAPI.EndPoints;

public static class GetUsersSync
{
    public record Response(int Id, string Name);

    public class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapGet("api/userssync", Handler);

        public IResult Handler(string? name, LoanDbContext db)
        {
            var result = string.IsNullOrWhiteSpace(name) ?
                            db.Users.ToList() :
                            db.Users.Where(u => u.Name.Contains(name)).ToList();

            return Results.Ok(result.Select(u => new Response(u.Id, u.Name)));
        }
    }
}