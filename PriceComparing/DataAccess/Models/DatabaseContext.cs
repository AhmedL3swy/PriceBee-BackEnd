﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using DataAccess.Interfaces;
using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;


namespace DataAccess.Models;

public partial class DatabaseContext : IdentityDbContext<AuthUser>
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Brand> Brands { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Domain> Domains { get; set; }

    public virtual DbSet<PriceHistory> PriceHistories { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductDetail> ProductDetails { get; set; }

    public virtual DbSet<ProductImage> ProductImages { get; set; }

    public virtual DbSet<ProductLink> ProductLinks { get; set; }

    public virtual DbSet<ProductSponsored> ProductSponsoreds { get; set; }

    public virtual DbSet<SearchValue> SearchValues { get; set; }

    public virtual DbSet<SubCategory> SubCategories { get; set; }

    // public virtual DbSet<User> websiteUsers { get; set; }

    public virtual DbSet<UserFavProd> SDUserFavProds { get; set; }



    public virtual DbSet<User> websiteUsers { get; set; }
    public DbSet<PaidProduct> PaidProducts { get; set; }





    //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
    //        => optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=ProdCompDatabase;Integrated Security=True");


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Brand>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Brands__3214EC070B848796");

            entity.HasOne(d => d.Category).WithMany(p => p.Brands)
                // .OnDelete(DeleteBehavior.ClientSetNull)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Brands__Category__06CD04F7");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Category__3214EC0750830B99");
        });

        modelBuilder.Entity<Domain>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Domain__3214EC0710D1138D");
        });

        modelBuilder.Entity<PriceHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PriceHis__3214EC0755FCD22C");

            entity.HasOne(d => d.Prod).WithMany(p => p.PriceHistories)
                // .OnDelete(DeleteBehavior.ClientSetNull)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__PriceHist__ProdI__3F466844");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Product__3214EC073BC2FE02");

            entity.HasOne(d => d.Brand).WithMany(p => p.Products).HasConstraintName("FK_Product_Brands");

            entity.HasOne(d => d.SubCategory).WithMany(p => p.Products)
                // .OnDelete(DeleteBehavior.ClientSetNull)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Product__SubCate__3C69FB99");
        });

        modelBuilder.Entity<PaidProduct>(entity =>
{
    entity.HasKey(e => e.Id).HasName("PK__PaidProduct__3214EC073BC2FE02");

    entity.HasOne(d => d.Brand).WithMany(p => p.PaidProducts).HasConstraintName("FK_PaidProduct_Brands");

    entity.HasOne(d => d.SubCategory).WithMany(p => p.PaidProducts)
          .OnDelete(DeleteBehavior.ClientSetNull)
          .HasConstraintName("FK__PaidProduct__SubCate__3C69FB99");

    // Additional configurations for PaidProduct specific properties
    // For example, setting a default value for IsPaid
    entity.Property(e => e.IsPaid).HasDefaultValue(true);
});


        modelBuilder.Entity<ProductDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ProductD__3214EC07F47B383F");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.ProductDetail)
                // .OnDelete(DeleteBehavior.ClientSetNull)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__ProductDe__Brand__47DBAE45");
        });

        modelBuilder.Entity<ProductImage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ProductI__3214EC075506E1BC");

            entity.HasOne(d => d.Prod).WithMany(p => p.ProductImages)
                // .OnDelete(DeleteBehavior.ClientSetNull)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_ProductImages_Product");
        });

        modelBuilder.Entity<ProductLink>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ProductL__3214EC07EA19D01C");

            entity.HasOne(d => d.Domain).WithMany(p => p.ProductLinks)
                // .OnDelete(DeleteBehavior.ClientSetNull)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__ProductLi__Domai__44FF419A");

            entity.HasOne(d => d.Prod).WithMany(p => p.ProductLinks)
                // .OnDelete(DeleteBehavior.ClientSetNull)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__ProductLi__ProdI__440B1D61");
        });

        modelBuilder.Entity<ProductSponsored>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ProductS__3214EC0736B50124");

            entity.HasOne(d => d.ProdDet).WithMany(p => p.ProductSponsoreds)
                // .OnDelete(DeleteBehavior.ClientSetNull)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__ProductSp__ProdI__4D94879B");
        });

        modelBuilder.Entity<SearchValue>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SearchVa__3214EC075A6F0F8F");

            entity.HasOne(d => d.User).WithMany(p => p.SearchValues)
                // .OnDelete(DeleteBehavior.ClientSetNull)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__SearchVal__UserI__534D60F1");
        });

        modelBuilder.Entity<SubCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SubCateg__3214EC07F20DA768");

            entity.HasOne(d => d.Category).WithMany(p => p.SubCategories)
                // .OnDelete(DeleteBehavior.ClientSetNull)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__SubCatego__Categ__398D8EEE");
        });

        modelBuilder.Entity<UserAlertProd>(entity =>
        {
            entity.HasKey(e=>new {e.UserID, e.ProductId })
                .HasName("PK__UserAler__57CAB4F28267EEDA");

            entity.HasOne(d => d.Product).WithMany(p => p.UserAlertProds)
                .HasForeignKey(d => d.ProductId)
                // .OnDelete(DeleteBehavior.ClientSetNull)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__UserAlert__ProdI__5AEE82B9");

        });

        modelBuilder.Entity<UserFavProd>(entity =>
        {
            entity.HasKey(e => new { e.UserID, e.ProductId })
                .HasName("PK__UserFavP__57CAB4F256C39848");

            entity.HasOne(d=>d.Product).WithMany(p=>p.UserFavProds)
                .HasForeignKey(d => d.ProductId)
                // .OnDelete(DeleteBehavior.ClientSetNull)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__UserFavPr__ProdI__571DF1D5");


        });

        modelBuilder.Entity<UserHistoryProd>(entity =>
        {
            entity.HasKey(e => new { e.UserID, e.ProductId })
                .HasName("PK__UserHist__57CAB4F28EC6E229");

            entity.HasOne(d => d.Product).WithMany(p => p.UserHistoryProds)
                .HasForeignKey(d => d.ProductId)
                // .OnDelete(DeleteBehavior.ClientSetNull)-
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__UserHisto__ProdI__5EBF139D");

        });

        base.OnModelCreating(modelBuilder);
        // OnModelCreatingPartial(modelBuilder);

        // Apply soft delete configuration to all entities that implement ISoftDeletable
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(ISoftDeletable).IsAssignableFrom(entityType.ClrType))
            {
                modelBuilder.Entity(entityType.ClrType)
                    .Property<bool>("IsDeleted")
                    .HasDefaultValue(false);

                // Creating the filter expression
                var parameter = Expression.Parameter(entityType.ClrType, "e");
                var propertyMethod = typeof(EF).GetMethod("Property").MakeGenericMethod(typeof(bool));
                var propertyAccess = Expression.Call(propertyMethod, parameter, Expression.Constant("IsDeleted"));
                var filter = Expression.Lambda(Expression.Equal(propertyAccess, Expression.Constant(false)), parameter);
                modelBuilder.Entity(entityType.ClrType).HasQueryFilter(filter);
            }
        }

       // OnModelCreatingPartial(modelBuilder);

    }



    // partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

