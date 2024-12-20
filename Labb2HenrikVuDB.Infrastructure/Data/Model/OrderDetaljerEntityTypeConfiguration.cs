using Labb2HenrikVuDB.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Labb2HenrikVuDB.Infrastructure.Data.Model;

public class OrderDetaljerEntityTypeConfiguration : IEntityTypeConfiguration<OrderDetaljer>
{
    public void Configure(EntityTypeBuilder<OrderDetaljer> builder)
    {
        builder.HasKey(e => e.OrderDetaljId).HasName("PK__OrderDet__94F5EAB5FC90CF93");

        builder.ToTable("OrderDetaljer");

        builder.Property(e => e.OrderDetaljId).HasColumnName("OrderDetaljID");
        builder.Property(e => e.Isbn)
            .HasMaxLength(13)
            .HasColumnName("ISBN");
        builder.Property(e => e.OrderId).HasColumnName("OrderID");
        builder.Property(e => e.Pris).HasColumnType("decimal(10, 2)");

        builder.HasOne(d => d.IsbnNavigation)
            .WithMany(p => p.OrderDetaljer)
            .HasForeignKey(d => d.Isbn)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK__OrderDetal__ISBN__1E05700A");

        builder.HasOne(d => d.Order)
            .WithMany(p => p.OrderDetaljer)
            .HasForeignKey(d => d.OrderId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK__OrderDeta__Order__1D114BD1");
    }
}
