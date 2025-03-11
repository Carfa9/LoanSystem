using Microsoft.EntityFrameworkCore;
using WebAPI.Infrastructure;

public class LoanDbContextSeed
{
    public static async Task SeedAsync(LoanDbContext dbContext)
    {
        if (!await dbContext.Users.AnyAsync())
        {
            await dbContext.Users.AddRangeAsync(
                [
                    new(){Name = "John"},
                    new(){Name = "Jane"},
                    new(){Name = "Doe"},
                    new(){Name = "Alice"},
                    new(){Name = "Bob"},
                    new(){Name = "Charlie"},
                    new(){Name = "David"},
                    new(){Name = "Eve"},
                    new(){Name = "Frank"},
                    new(){Name = "Grace"},
                ]);
        }

        await dbContext.SaveChangesAsync();

        if (!await dbContext.Items.AnyAsync())
        {
            await dbContext.Items.AddRangeAsync(
                [
                    new("Mugg", "En delvis använd kaffemugg. Eller heter det kopp?", 1),
                    new("T-Shirt", "En T-Shirt från Florida. Aldrig använd, om du inte lånar den.", 2),
                    new("Sheet", "Ett sånt som amerikanarna kallar ett sheet.", 3),
                    new("USB-minne", "Ett USB-minne med diverse filer. Ta inte bort något!!!", 4)
                ]);
        }

        await dbContext.SaveChangesAsync();
    }
}
