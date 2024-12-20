using Labb2HenrikVuDB.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Labb2HenrikVuDB.Infrastructure.Data.Model;

public class BeställareEntityTypeConfiguration : IEntityTypeConfiguration<Beställare>
{
    public void Configure(EntityTypeBuilder<Beställare> builder)
    {
        builder.HasKey(e => e.UserId).HasName("PK__Beställa__1788CCACD78F3E5E");

        builder.ToTable("Beställare");

        builder.Property(e => e.UserId).HasColumnName("UserID");
    }
}
