using WebAPI.Infrastructure;

namespace WebAPI.EndPoints;

public static class ReturnItem
{
    public record Request(int ItemId, int BorrowerId);

    public class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapPost("api/items/return", Handler);

        public static async Task<IResult> Handler(Request request, LoanDbContext db)
        {
            var item = await db.Items.FindAsync(request.ItemId);
            if (item == null)
            {
                return Results.NotFound();
            }

            var borrower = await db.Users.FindAsync(request.BorrowerId);
            if (borrower == null)
            {
                return Results.NotFound();
            }

            if (!item.Return(borrower.Id))
            {
                return Results.BadRequest("Item is not borrowed by the specified user.");
            }

            await db.SaveChangesAsync();

            return Results.Ok(new { item.Id, item.BorrowerId });
        }
    }
}