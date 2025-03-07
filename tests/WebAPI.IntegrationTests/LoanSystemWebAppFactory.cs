using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using WebAPI.Infrastructure;
using WebAPI.Models;

public class LoanSystemWebAppFactory : WebApplicationFactory<User>
{
    private string _dbPath = "";

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            //Ta bort alla LoanDbContextrelaterade konfigurationer
            services.RemoveAll<IDbContextOptionsConfiguration<LoanDbContext>>();

            _dbPath = Path.Combine(Path.GetTempPath(), $"ZZZtest{Guid.NewGuid()}.db");
            Console.WriteLine(_dbPath);
            services.AddDbContext<LoanDbContext>(options => options.UseSqlite($"Data Source={_dbPath}"));

            //Skapa databasen
            using var scope = services.BuildServiceProvider().CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<LoanDbContext>();
            dbContext.Database.EnsureCreated();

            //Seeda databasen
            User user = new User { Name = "John" };
            dbContext.Users.Add(user);
            dbContext.Users.Add(new User { Name = "Jane" });
            dbContext.Users.Add(new User { Name = "Bengt" });
            dbContext.Users.Add(new User { Name = "Ulla" });
            dbContext.Users.Add(new User { Name = "Sinan" });

            dbContext.SaveChanges();

            Item enBraBok = new Item("En Populär Bok", "En riktigt bra bok", 1);
            dbContext.Items.Add(enBraBok);
            dbContext.Items.Add(new Item("Bok", "En annan bok", 1));
            dbContext.Items.Add(new Item("Dator", "En dator", 2));
            dbContext.Items.Add(new Item("Bil", "En bil", 3));
            dbContext.Items.Add(new Item("Cykel", "En cykel", 4));
            dbContext.Items.Add(new Item("Sko", "En högersko", 5));
            dbContext.Items.Add(new Item("Sko", "En vänstersko", 5));

            enBraBok.Borrow(user);
            dbContext.SaveChanges();
        });
    }
}

public class WebAppFactoryFixture : IDisposable
{
    public LoanSystemWebAppFactory Factory { get; private set; }

    public WebAppFactoryFixture()
    {
        Factory = new LoanSystemWebAppFactory();
    }

    public void Dispose()
    {
        using var scope = Factory.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<LoanDbContext>();
        dbContext.Database.EnsureDeleted();

        Factory.Dispose();
        Factory = null!;
    }
}