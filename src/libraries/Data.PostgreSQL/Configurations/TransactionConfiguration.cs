using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.PostgreSQL.Configurations;

public class TransactionConfiguration : IEntityTypeConfiguration<Entities.Transaction>
{
    public void Configure(EntityTypeBuilder<Entities.Transaction> builder)
    {
        builder.ToTable("transaction", "public");
        
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder.Property(x => x.FromWalletId)
            .IsRequired();
        
        builder.Property(x => x.ToWalletId)
            .IsRequired();
        
        builder.Property(x => x.CardNumber)
            .IsRequired();
        
        builder.Property(x => x.FirstName)
            .IsRequired();
        
        builder.Property(x => x.LastName)
            .IsRequired();
        
        builder.Property(x => x.Email)
            .IsRequired();
        
        builder.Property(x => x.Total)
            .IsRequired();
        
        builder.Property(x => x.CreatedOnUtc)
            .IsRequired()
            .HasDefaultValue(DateTimeOffset.UtcNow);
    }
}
