using Labb2HenrikVuDB.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Labb2HenrikVuDB.Infrastructure.Data.Model;

public class LagerSaldoEntityTypeConfiguration : IEntityTypeConfiguration<LagerSaldo>
{
    public void Configure(EntityTypeBuilder<LagerSaldo> builder)
    {
        builder.HasKey(e => new { e.ButikId, e.Isbn }).HasName("PK__LagerSal__1191B8941FADA22C");

        builder.ToTable("LagerSaldo");

        builder.Property(e => e.ButikId).HasColumnName("ButikID");
        builder.Property(e => e.Isbn)
            .HasMaxLength(13)
            .HasColumnName("ISBN");

        builder.HasOne(d => d.Butik)
            .WithMany(p => p.LagerSaldo)
            .HasForeignKey(d => d.ButikId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK__LagerSald__Butik__2D7CBDC4");

        builder.HasOne(d => d.IsbnNavigation)
            .WithMany(p => p.LagerSaldo)
            .HasForeignKey(d => d.Isbn)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK__LagerSaldo__ISBN__2E70E1FD");
    }
}
