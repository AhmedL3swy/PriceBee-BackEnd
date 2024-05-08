#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public partial class DBContext : DbContext
{
    public DBContext()
    {
    }

    public DBContext(DbContextOptions<DBContext> options)
        : base(options)
    {
    }

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

    public virtual DbSet<User> Users { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=ProductComparingDB;Integrated Security=True;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
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
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PriceHist__ProdI__3F466844");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Product__3214EC073BC2FE02");

            entity.HasOne(d => d.SubCategory).WithMany(p => p.Products)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Product__SubCate__3C69FB99");
        });

        modelBuilder.Entity<ProductDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ProductD__3214EC07F47B383F");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.ProductDetail)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ProductDe__Brand__47DBAE45");
        });

        modelBuilder.Entity<ProductImage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ProductI__3214EC075506E1BC");

            entity.HasOne(d => d.Prod).WithMany(p => p.ProductImages)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ProductIm__ProdI__4AB81AF0");
        });

        modelBuilder.Entity<ProductLink>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ProductL__3214EC07EA19D01C");

            entity.HasOne(d => d.Domain).WithMany(p => p.ProductLinks)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ProductLi__Domai__44FF419A");

            entity.HasOne(d => d.Prod).WithMany(p => p.ProductLinks)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ProductLi__ProdI__440B1D61");
        });

        modelBuilder.Entity<ProductSponsored>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ProductS__3214EC0736B50124");

            entity.HasOne(d => d.Prod).WithMany(p => p.ProductSponsoreds)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ProductSp__ProdI__4D94879B");
        });

        modelBuilder.Entity<SearchValue>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SearchVa__3214EC075A6F0F8F");

            entity.HasOne(d => d.User).WithMany(p => p.SearchValues)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SearchVal__UserI__534D60F1");
        });

        modelBuilder.Entity<SubCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SubCateg__3214EC07F20DA768");

            entity.HasOne(d => d.Category).WithMany(p => p.SubCategories)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SubCatego__Categ__398D8EEE");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC07DA5FEBAD");

            entity.HasMany(d => d.Prods).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "UserAlertProd",
                    r => r.HasOne<Product>().WithMany()
                        .HasForeignKey("ProdId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__UserAlert__ProdI__5AEE82B9"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__UserAlert__ProdI__59FA5E80"),
                    j =>
                    {
                        j.HasKey("UserID", "ProdId").HasName("PK__UserAler__57CAB4F28267EEDA");
                        j.ToTable("UserAlertProd");
                    });

            entity.HasMany(d => d.Prods1).WithMany(p => p.Users1)
                .UsingEntity<Dictionary<string, object>>(
                    "UserHistoryProd",
                    r => r.HasOne<Product>().WithMany()
                        .HasForeignKey("ProdId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__UserHisto__ProdI__5EBF139D"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__UserHisto__ProdI__5DCAEF64"),
                    j =>
                    {
                        j.HasKey("UserID", "ProdId").HasName("PK__UserHist__57CAB4F28EC6E229");
                        j.ToTable("UserHistoryProd");
                    });

            entity.HasMany(d => d.ProdsNavigation).WithMany(p => p.UsersNavigation)
                .UsingEntity<Dictionary<string, object>>(
                    "UserFavProd",
                    r => r.HasOne<Product>().WithMany()
                        .HasForeignKey("ProdId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__UserFavPr__ProdI__571DF1D5"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__UserFavPr__ProdI__5629CD9C"),
                    j =>
                    {
                        j.HasKey("UserID", "ProdId").HasName("PK__UserFavP__57CAB4F256C39848");
                        j.ToTable("UserFavProd");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}