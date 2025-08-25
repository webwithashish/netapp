using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Infosys.DBFirstCore.DataAccessLayer.Models;

public partial class PricingDbContext : DbContext
{
    public PricingDbContext()
    {
    }

    public PricingDbContext(DbContextOptions<PricingDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Marker> Markers { get; set; }

    public virtual DbSet<MarkerPrice> MarkerPrices { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductDiff> ProductDiffs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source =(localdb)\\MSSQLLocalDB;Initial Catalog=PricingDB;Integrated Security=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Marker>(entity =>
        {
            entity.HasKey(e => e.MarkerId).HasName("PK__Markers__743D929DEA0339D2");

            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);
        });

        modelBuilder.Entity<MarkerPrice>(entity =>
        {
            entity.HasKey(e => e.MarkerPriceId).HasName("PK__MarkerPr__AF33A5A921E66D88");

            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Value).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Marker).WithMany(p => p.MarkerPrices)
                .HasForeignKey(d => d.MarkerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_marker");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Products__B40CC6CD8C52D9BD");

            entity.Property(e => e.BasePrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);
        });

        modelBuilder.Entity<ProductDiff>(entity =>
        {
            entity.HasKey(e => e.ProductDiffId).HasName("PK__ProductD__E9C834B3A9169ECC");

            entity.Property(e => e.DiffValue).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.Marker).WithMany(p => p.ProductDiffs)
                .HasForeignKey(d => d.MarkerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_marker_diff");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductDiffs)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_product");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
