using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Infrastructure;

public class LoanDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Item> Items { get; set; }

    public LoanDbContext(DbContextOptions<LoanDbContext> options) : base(options) { }
}