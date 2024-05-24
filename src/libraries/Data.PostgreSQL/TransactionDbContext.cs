using Microsoft.EntityFrameworkCore;

namespace Data.PostgreSQL;

public class TransactionDbContext : DbContext
{
    // dotnet ef migrations add v1.0.0 -c Data.PostgreSQL.TransactionDbContext -p ../Write.Api
    // dotnet ef migrations script 0 v1.0.0 -c Data.PostgreSQL.TransactionDbContext -p ../Write.Api

    public TransactionDbContext(DbContextOptions<TransactionDbContext> options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    public DbSet<Entities.Transaction> Transaction { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("public");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TransactionDbContext).Assembly);
        
        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        return await base.SaveChangesAsync(cancellationToken);
    }
}
