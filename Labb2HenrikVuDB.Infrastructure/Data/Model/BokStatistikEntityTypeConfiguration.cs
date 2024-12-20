using Labb2HenrikVuDB.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Labb2HenrikVuDB.Infrastructure.Data.Model;

public class BokStatistikEntityTypeConfiguration : IEntityTypeConfiguration<BokStatistik>
{
    public void Configure(EntityTypeBuilder<BokStatistik> builder)
    {
        builder
            .HasNoKey()
            .ToView("BokStatistik");

        builder.Property(e => e.AntalBeställningar).HasColumnName("Antal Beställningar");
        builder.Property(e => e.AntalRecensioner).HasColumnName("Antal Recensioner");
        builder.Property(e => e.Isbn)
            .HasMaxLength(13)
            .HasColumnName("ISBN");
    }
}
