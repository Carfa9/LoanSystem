using Microsoft.EntityFrameworkCore;
using WebAPI.Infrastructure;

namespace WebAPI.EndPoints;

public static class GetItems
{
    public class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) =>
            app.MapGet("api/items", Handler);

        public static async Task<IResult> Handler(LoanDbContext db, bool availableOnly = false)
        {
            var items = availableOnly ?
                            await db.Items.Where(i => i.BorrowerId != null).ToListAsync() :
                            await db.Items.ToListAsync();

            return Results.Ok(items.Select(i => new { i.Id, i.Name, i.Description, i.OwnerId }));
        }
    }
}