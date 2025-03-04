public static class GetUserByID
{
    public record Response(User User);

    public class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapPost("api/users/{id:int}", Handler);

        public static IResult Handler(int id, LoanDbContext db)
        {
            User? user = db.Users.Find(id);

            if (user is null)
            {
                return Results.NotFound();
            }

            return Results.Ok(new Response(user));
        }
    }
}