using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using Labb2HenrikVuDB.Domain;
using Microsoft.EntityFrameworkCore;

namespace Labb2HenrikVuDB.Infrastructure.Data.Model;

public partial class Labb1HenrikVuContext : DbContext
{
    public Labb1HenrikVuContext()
    {
    }

    public Labb1HenrikVuContext(DbContextOptions<Labb1HenrikVuContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Beställare> Beställare { get; set; }

    public virtual DbSet<Beställningar> Beställningar { get; set; }

    public virtual DbSet<BokRecensioner> BokRecensioner { get; set; }

    public virtual DbSet<BokStatistik> BokStatistik { get; set; }

    public virtual DbSet<Butiker> Butiker { get; set; }

    public virtual DbSet<Böcker> Böcker { get; set; }

    public virtual DbSet<Författare> Författare { get; set; }

    public virtual DbSet<Genrar> Genrar { get; set; }

    public virtual DbSet<LagerSaldo> LagerSaldo { get; set; }

    public virtual DbSet<OrderDetaljer> OrderDetaljer { get; set; }

    public virtual DbSet<TitlarPerFörfattare> TitlarPerFörfattare { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Initial Catalog=Labb1 HenrikVu;Integrated Security=True;Trust Server Certificate=True;Server SPN=localhost");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        new BeställareEntityTypeConfiguration().Configure(modelBuilder.Entity<Beställare>());
        new BeställningarEntityTypeConfiguration().Configure(modelBuilder.Entity<Beställningar>());
        new BokRecensionerEntityTypeConfiguration().Configure(modelBuilder.Entity<BokRecensioner>());
        new BokStatistikEntityTypeConfiguration().Configure(modelBuilder.Entity<BokStatistik>());
        new ButikerEntityTypeConfiguration().Configure(modelBuilder.Entity<Butiker>());
        new BöckerEntityTypeConfiguration().Configure(modelBuilder.Entity<Böcker>());
        new FörfattareEntityTypeConfiguration().Configure(modelBuilder.Entity<Författare>());
        new GenrarEntityTypeConfiguration().Configure(modelBuilder.Entity<Genrar>());
        new LagerSaldoEntityTypeConfiguration().Configure(modelBuilder.Entity<LagerSaldo>());
        new OrderDetaljerEntityTypeConfiguration().Configure(modelBuilder.Entity<OrderDetaljer>());
        new TitlarPerFörfattareEntityTypeConfiguration().Configure(modelBuilder.Entity<TitlarPerFörfattare>());

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
