using Microsoft.EntityFrameworkCore;

public static class GetUsers
{
    public class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapGet("api/users", Handler);

        public static async Task<IResult> Handler(string? name, LoanDbContext db)
        {
            var result = string.IsNullOrWhiteSpace(name) ?
                            await db.Users.ToListAsync() :
                            await db.Users.Where(u => u.Name.Contains(name)).ToListAsync();

            return Results.Ok(result.Select(u => new { u.Id, u.Name }));
        }
    }
}