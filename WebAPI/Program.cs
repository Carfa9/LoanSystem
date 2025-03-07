using WebAPI.EndPoints;
using WebAPI.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<LoanDbContext>(options => options.UseSqlite("Data Source=database.db"));
builder.Services.AddEndpoints(typeof(Program).Assembly);

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapEndpoints();

app.Run();