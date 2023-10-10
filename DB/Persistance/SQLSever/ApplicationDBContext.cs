using DB.Models;
using Microsoft.EntityFrameworkCore;

namespace DB.Persistance.SQLSever;

public class ApplicationDBContext : DbContext
{
    public ApplicationDBContext(DbContextOptions options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Card>().Property(k => k.CardNumber)
            .HasMaxLength(15)
            .IsFixedLength();
        modelBuilder.Entity<Card>().Property(k => k.Balance)
            .HasDefaultValue(1);
    }
    public DbSet<Card> Cards => Set<Card>();
}
