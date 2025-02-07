using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Denna rad krävs för att använda API Controller-klasser
builder.Services.AddControllers();
builder.Services.AddDbContext<LoanDbContext>(options => options.UseInMemoryDatabase("LoanDb"));

var app = builder.Build();

app.MapGet("status", () => 0);

//Denna rad krävs för att använda API Controller-klasser
app.MapControllers();

app.Run();
