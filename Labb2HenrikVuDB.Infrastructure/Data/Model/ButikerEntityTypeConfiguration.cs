using Labb2HenrikVuDB.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Labb2HenrikVuDB.Infrastructure.Data.Model;

public class ButikerEntityTypeConfiguration : IEntityTypeConfiguration<Butiker>
{
    public void Configure(EntityTypeBuilder<Butiker> builder)
    {
        builder.HasKey(e => e.Id).HasName("PK__Butiker__3214EC27E5036FBC");

        builder.ToTable("Butiker");

        builder.Property(e => e.Id).HasColumnName("ID");
    }
}
