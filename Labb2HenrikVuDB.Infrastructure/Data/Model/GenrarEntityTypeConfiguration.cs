using Labb2HenrikVuDB.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Labb2HenrikVuDB.Infrastructure.Data.Model;

public class GenrarEntityTypeConfiguration : IEntityTypeConfiguration<Genrar>
{
    public void Configure(EntityTypeBuilder<Genrar> builder)
    {
        builder.HasKey(e => e.GenreId).HasName("PK__Genres__0385055E9F33E70B");

        builder.ToTable("Genrar");

        builder.Property(e => e.GenreId).HasColumnName("GenreID");
    }
}
