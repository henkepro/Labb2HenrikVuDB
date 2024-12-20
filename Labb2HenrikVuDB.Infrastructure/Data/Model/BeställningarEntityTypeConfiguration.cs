using Labb2HenrikVuDB.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Labb2HenrikVuDB.Infrastructure.Data.Model;

public class BeställningarEntityTypeConfiguration : IEntityTypeConfiguration<Beställningar>
{
    public void Configure(EntityTypeBuilder<Beställningar> builder)
    {
        builder.HasKey(e => e.OrderId).HasName("PK__Beställn__C3905BAF63F36498");

        builder.ToTable("Beställningar");

        builder.Property(e => e.OrderId).HasColumnName("OrderID");
        builder.Property(e => e.BeställareId).HasColumnName("BeställareID");
        builder.Property(e => e.ButikId).HasColumnName("ButikID");

        builder.HasOne(d => d.Beställare)
            .WithMany(p => p.Beställningar)
            .HasForeignKey(d => d.BeställareId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK__Beställni__Bestä__147C05D0");

        builder.HasOne(d => d.Butik)
            .WithMany(p => p.Beställningar)
            .HasForeignKey(d => d.ButikId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK__Beställni__Butik__15702A09");
    }
}
