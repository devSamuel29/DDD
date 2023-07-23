using PROJETO.Infra.Models;

using Microsoft.EntityFrameworkCore;

namespace PROJETO.Infra.Database;

public class MyDbContext : DbContext
{
    public MyDbContext(DbContextOptions options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MyDbContext).Assembly);
    }

    public DbSet<UserModel> Users { get; set; } = null!;
}
