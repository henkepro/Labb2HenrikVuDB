using Labb2HenrikVuDB.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Labb2HenrikVuDB.Infrastructure.Data.Model;

public class BöckerEntityTypeConfiguration : IEntityTypeConfiguration<Böcker>
{
    public void Configure(EntityTypeBuilder<Böcker> builder)
    {
        builder.HasKey(e => e.Isbn13).HasName("PK__Böcker__3BF79E03585C9C3D");

        builder.ToTable("Böcker");

        builder.Property(e => e.Isbn13)
            .HasMaxLength(13)
            .HasColumnName("ISBN13");
        builder.Property(e => e.Pris).HasColumnType("decimal(10, 2)");

        builder.HasMany(d => d.Genrar)
            .WithMany(p => p.Isbn)
            .UsingEntity<Dictionary<string, object>>(
                "BokGenrar",
                r => r.HasOne<Genrar>().WithMany()
                    .HasForeignKey("Genre")
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BookGenre__Genre__60FC61CA"),
                l => l.HasOne<Böcker>().WithMany()
                    .HasForeignKey("Isbn")
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BookGenres__ISBN__60083D91"),
                j =>
                {
                    j.HasKey("Isbn", "Genre").HasName("PK__BookGenr__7B6926248163565C");
                    j.ToTable("BokGenrar");
                    j.IndexerProperty<string>("Isbn")
                        .HasMaxLength(13)
                        .HasColumnName("ISBN");
                });
    }
}
