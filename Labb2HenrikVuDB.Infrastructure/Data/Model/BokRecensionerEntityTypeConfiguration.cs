using Labb2HenrikVuDB.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Labb2HenrikVuDB.Infrastructure.Data.Model;

public class BokRecensionerEntityTypeConfiguration : IEntityTypeConfiguration<BokRecensioner>
{
    public void Configure(EntityTypeBuilder<BokRecensioner> builder)
    {
        builder.HasKey(e => e.RecensionsId).HasName("PK__BookRece__0242D6F3CEE8A8AF");

        builder.ToTable("BokRecensioner");

        builder.Property(e => e.RecensionsId).HasColumnName("RecensionsID");
        builder.Property(e => e.Isbn)
            .HasMaxLength(13)
            .HasColumnName("ISBN");
        builder.Property(e => e.UserId).HasColumnName("UserID");

        builder.HasOne(d => d.IsbnNavigation)
            .WithMany(p => p.BokRecensioner)
            .HasForeignKey(d => d.Isbn)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK__BookRecens__ISBN__76EBA2E9");

        builder.HasOne(d => d.User)
            .WithMany(p => p.BokRecensioners)
            .HasForeignKey(d => d.UserId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK__BookRecen__UserI__75F77EB0");
    }
}
