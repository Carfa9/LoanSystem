
public static class BorrowItem
{
    public record Request(int ItemId, int BorrowerId);

    public class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapPost("api/items/borrow", Handler);

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

            if (!item.Borrow(borrower))
            {
                return Results.BadRequest("Item is not available for borrowing.");
            }

            await db.SaveChangesAsync();

            return Results.Ok(new { item.Id, item.BorrowerId });
        }
    }
}