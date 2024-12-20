using Labb2HenrikVuDB.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Labb2HenrikVuDB.Infrastructure.Data.Model;

public class FörfattareEntityTypeConfiguration : IEntityTypeConfiguration<Författare>
{
    public void Configure(EntityTypeBuilder<Författare> builder)
    {
        builder.HasKey(e => e.Id).HasName("PK__Författa__3214EC27DFA3E6EB");

        builder.ToTable("Författare");

        builder.Property(e => e.Id).HasColumnName("ID");
        builder.Property(e => e.Efternamn).HasMaxLength(50);
        builder.Property(e => e.Förnamn).HasMaxLength(50);

        builder.HasMany(d => d.Isbn13)
            .WithMany(p => p.Författare)
            .UsingEntity<Dictionary<string, object>>(
                "FörfattareBöcker",
                r => r.HasOne<Böcker>().WithMany()
                    .HasForeignKey("Isbn13")
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Författar__ISBN1__5D2BD0E6"),
                l => l.HasOne<Författare>().WithMany()
                    .HasForeignKey("FörfattareId")
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Författar__Förfa__5C37ACAD"),
                j =>
                {
                    j.HasKey("FörfattareId", "Isbn13").HasName("PK__Författa__A3FE6D30A71DC336");
                    j.ToTable("Författare_Böcker");
                    j.IndexerProperty<int>("FörfattareId").HasColumnName("FörfattareID");
                    j.IndexerProperty<string>("Isbn13")
                        .HasMaxLength(13)
                        .HasColumnName("ISBN13");
                });
    }
}
