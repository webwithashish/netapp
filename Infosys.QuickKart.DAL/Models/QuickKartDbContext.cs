using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infosys.QuickKart.DAL.Models;

public partial class QuickKartDbContext : DbContext
{
    public QuickKartDbContext()
    {
    }

    public QuickKartDbContext(DbContextOptions<QuickKartDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CardDetail> CardDetails { get; set; }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductDetail> ProductDetails { get; set; }

    public virtual DbSet<PurchaseDetail> PurchaseDetails { get; set; }

    public virtual DbSet<Rating> Ratings { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<CartProduct> CartProducts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var builder = new ConfigurationBuilder()
                          .SetBasePath(Directory.GetCurrentDirectory())
                          .AddJsonFile("appsettings.json");
        var config = builder.Build();
        var connectionString = config.GetConnectionString("QuickKartDBConnectionString");
        if (!optionsBuilder.IsConfigured)
        {
            // #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
            optionsBuilder.UseSqlServer(connectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CardDetail>(entity =>
        {
            entity.HasKey(e => e.CardNumber).HasName("pk_CardNumber");

            entity.Property(e => e.CardNumber).HasColumnType("numeric(16, 0)");
            entity.Property(e => e.Balance).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.CardType)
                .IsRequired()
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Cvvnumber)
                .HasColumnType("numeric(3, 0)")
                .HasColumnName("CVVNumber");
            entity.Property(e => e.NameOnCard)
                .IsRequired()
                .HasMaxLength(40)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => new { e.ProductId, e.EmailId }).HasName("pk_ProductId_EmailId");

            entity.ToTable("Cart");

            entity.Property(e => e.ProductId)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.EmailId)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Email).WithMany(p => p.Carts)
                .HasForeignKey(d => d.EmailId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Cart_EmailId");

            entity.HasOne(d => d.Product).WithMany(p => p.Carts)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Cart_ProductId");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("pk_CategoryId");

            entity.HasIndex(e => e.CategoryName, "uq_CategoryName").IsUnique();

            entity.Property(e => e.CategoryId).ValueGeneratedOnAdd();
            entity.Property(e => e.CategoryName)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("pk_ProductId");

            entity.HasIndex(e => e.CategoryId, "ix_CategoryId");

            entity.HasIndex(e => e.ProductName, "uq_ProductName").IsUnique();

            entity.Property(e => e.ProductId)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Price).HasColumnType("numeric(8, 0)");
            entity.Property(e => e.ProductName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("fk_CategoryId");
        });

        modelBuilder.Entity<ProductDetail>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.ProductId)
                .IsRequired()
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.ProductName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Seller)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Product).WithMany()
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ProdId");
        });

        modelBuilder.Entity<PurchaseDetail>(entity =>
        {
            entity.HasKey(e => e.PurchaseId).HasName("pk_PurchaseId");

            entity.HasIndex(e => e.EmailId, "ix_EmailId");

            entity.HasIndex(e => e.ProductId, "ix_ProductId");

            entity.Property(e => e.DateOfPurchase)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.EmailId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ProductId)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.Email).WithMany(p => p.PurchaseDetails)
                .HasForeignKey(d => d.EmailId)
                .HasConstraintName("fk_EmailId");

            entity.HasOne(d => d.Product).WithMany(p => p.PurchaseDetails)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("fk_ProductId");
        });

        modelBuilder.Entity<Rating>(entity =>
        {
            entity.HasKey(e => new { e.EmailId, e.ProductId }).HasName("pk_EmailId_ProductId_Rating");

            entity.ToTable("Rating");

            entity.Property(e => e.EmailId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ProductId)
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.ProductName)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Email).WithMany(p => p.Ratings)
                .HasForeignKey(d => d.EmailId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Rating_Users_EmailId");

            entity.HasOne(d => d.Product).WithMany(p => p.RatingProducts)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Rating_Products_ProductId");

            entity.HasOne(d => d.ProductNameNavigation).WithMany(p => p.RatingProductNameNavigations)
                .HasPrincipalKey(p => p.ProductName)
                .HasForeignKey(d => d.ProductName)
                .HasConstraintName("fk_Rating_Products_ProductName");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("pk_RoleId");

            entity.HasIndex(e => e.RoleName, "uq_RoleName").IsUnique();

            entity.Property(e => e.RoleId).ValueGeneratedOnAdd();
            entity.Property(e => e.RoleName)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.EmailId).HasName("pk_EmailId");

            entity.HasIndex(e => e.RoleId, "ix_RoleId");

            entity.Property(e => e.EmailId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Address)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Gender)
                .IsRequired()
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.UserPassword)
                .IsRequired()
                .HasMaxLength(15)
                .IsUnicode(false);

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("fk_RoleId");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
