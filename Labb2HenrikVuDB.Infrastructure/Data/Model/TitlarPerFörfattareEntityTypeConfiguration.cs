using Labb2HenrikVuDB.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Labb2HenrikVuDB.Infrastructure.Data.Model;

public class TitlarPerFörfattareEntityTypeConfiguration : IEntityTypeConfiguration<TitlarPerFörfattare>
{
    public void Configure(EntityTypeBuilder<TitlarPerFörfattare> builder)
    {
        builder
            .HasNoKey()
            .ToView("TitlarPerFörfattare");

        builder.Property(e => e.LagerVärde)
            .HasMaxLength(44)
            .IsUnicode(false);
        builder.Property(e => e.Namn).HasMaxLength(101);
        builder.Property(e => e.Titlar)
            .HasMaxLength(15)
            .IsUnicode(false);
        builder.Property(e => e.Ålder)
            .HasMaxLength(15)
            .IsUnicode(false);
    }
}
