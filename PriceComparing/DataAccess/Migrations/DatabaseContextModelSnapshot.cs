﻿// <auto-generated />
using System;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataAccess.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DataAccess.Models.AuthUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateOnly>("DateOfBirth")
                        .HasColumnType("date");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateOnly>("JoinDate")
                        .HasColumnType("date");

                    b.Property<string>("LName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneCode")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.HasIndex(new[] { "Email" }, "UQ__Users__A9D10534A011E897")
                        .IsUnique()
                        .HasFilter("[Email] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("DataAccess.Models.Brand", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description_Global")
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Description_Local")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("Logo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name_Global")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name_Local")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id")
                        .HasName("PK__Brands__3214EC070B848796");

                    b.HasIndex(new[] { "CategoryId" }, "IX_Brands_CategoryId");

                    b.ToTable("Brands");
                });

            modelBuilder.Entity("DataAccess.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("Name_Global")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name_Local")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id")
                        .HasName("PK__Category__3214EC0750830B99");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("DataAccess.Models.Domain", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description_Global")
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Description_Local")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("Logo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name_Global")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name_Local")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id")
                        .HasName("PK__Domain__3214EC0710D1138D");

                    b.ToTable("Domain");
                });

            modelBuilder.Entity("DataAccess.Models.PriceHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<int>("ProdId")
                        .HasColumnType("int");

                    b.HasKey("Id")
                        .HasName("PK__PriceHis__3214EC0755FCD22C");

                    b.HasIndex(new[] { "ProdId" }, "IX_PriceHistory_ProdId");

                    b.ToTable("PriceHistory");
                });

            modelBuilder.Entity("DataAccess.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("BrandId")
                        .HasColumnType("int");

                    b.Property<string>("Description_Global")
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Description_Local")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("Name_Global")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name_Local")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("SubCategoryId")
                        .HasColumnType("int");

                    b.HasKey("Id")
                        .HasName("PK__Product__3214EC073BC2FE02");

                    b.HasIndex(new[] { "BrandId" }, "IX_Product_BrandId");

                    b.HasIndex(new[] { "SubCategoryId" }, "IX_Product_SubCategoryId");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("DataAccess.Models.ProductDetail", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Brand")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Description_Global")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description_Local")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("Name_Global")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Name_Local")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<decimal?>("Rating")
                        .HasColumnType("decimal(3, 2)");

                    b.Property<bool>("isAvailable")
                        .HasColumnType("bit");

                    b.HasKey("Id")
                        .HasName("PK__ProductD__3214EC07F47B383F");

                    b.ToTable("ProductDetails");
                });

            modelBuilder.Entity("DataAccess.Models.ProductImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<int>("ProdId")
                        .HasColumnType("int");

                    b.HasKey("Id")
                        .HasName("PK__ProductI__3214EC075506E1BC");

                    b.HasIndex(new[] { "ProdId" }, "IX_ProductImages_ProdId");

                    b.ToTable("ProductImages");
                });

            modelBuilder.Entity("DataAccess.Models.ProductLink", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DomainId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<DateTime>("LastScraped")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("datetime");

                    b.Property<int>("ProdId")
                        .HasColumnType("int");

                    b.Property<string>("ProductLink1")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("ProductLink");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id")
                        .HasName("PK__ProductL__3214EC07EA19D01C");

                    b.HasIndex(new[] { "DomainId" }, "IX_ProductLinks_DomainId");

                    b.HasIndex(new[] { "ProdId" }, "IX_ProductLinks_ProdId");

                    b.ToTable("ProductLinks");
                });

            modelBuilder.Entity("DataAccess.Models.ProductSponsored", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Cost")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<int>("Duration")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<int>("ProdDetId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime");

                    b.HasKey("Id")
                        .HasName("PK__ProductS__3214EC0736B50124");

                    b.HasIndex(new[] { "ProdDetId" }, "IX_ProductSponsored_ProdId");

                    b.ToTable("ProductSponsored");
                });

            modelBuilder.Entity("DataAccess.Models.SearchValue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("SearchValue1")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("SearchValue");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("Id")
                        .HasName("PK__SearchVa__3214EC075A6F0F8F");

                    b.HasIndex(new[] { "UserID" }, "IX_SearchValues_UserID");

                    b.ToTable("SearchValues");
                });

            modelBuilder.Entity("DataAccess.Models.SubCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("Name_Global")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name_Local")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id")
                        .HasName("PK__SubCateg__3214EC07F20DA768");

                    b.HasIndex(new[] { "CategoryId" }, "IX_SubCategory_CategoryId");

                    b.ToTable("SubCategory");
                });

            modelBuilder.Entity("DataAccess.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AuthUserID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.HasKey("Id")
                        .HasName("PK__Users__3214EC07DA5FEBAD");

                    b.HasIndex("AuthUserID")
                        .IsUnique()
                        .HasFilter("[AuthUserID] IS NOT NULL");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "5d1f3368-342a-49a9-8dac-db1c2a140e5c",
                            ConcurrencyStamp = "1bf99acc-bb3b-4931-95f5-e2c58b3ef859",
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = "5e950883-a632-460a-8b3d-399a678d8282",
                            ConcurrencyStamp = "3442a882-25bb-4854-ba0f-8070b357e24e",
                            Name = "User",
                            NormalizedName = "USER"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("ProductUser", b =>
                {
                    b.Property<int>("ProdId")
                        .HasColumnType("int");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("ProdId", "UserID");

                    b.ToTable("ProductUser");
                });

            modelBuilder.Entity("ProductUser1", b =>
                {
                    b.Property<int>("ProdId")
                        .HasColumnType("int");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("ProdId", "UserID");

                    b.ToTable("ProductUser1");
                });

            modelBuilder.Entity("ProductUser2", b =>
                {
                    b.Property<int>("ProdId")
                        .HasColumnType("int");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("ProdId", "UserID");

                    b.ToTable("ProductUser2");
                });

            modelBuilder.Entity("UserAlertProd", b =>
                {
                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.Property<int>("ProdId")
                        .HasColumnType("int");

                    b.HasKey("UserID", "ProdId")
                        .HasName("PK__UserAler__57CAB4F28267EEDA");

                    b.HasIndex(new[] { "ProdId" }, "IX_UserAlertProd_ProdId");

                    b.ToTable("UserAlertProd", (string)null);
                });

            modelBuilder.Entity("UserFavProd", b =>
                {
                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.Property<int>("ProdId")
                        .HasColumnType("int");

                    b.HasKey("UserID", "ProdId")
                        .HasName("PK__UserFavP__57CAB4F256C39848");

                    b.HasIndex(new[] { "ProdId" }, "IX_UserFavProd_ProdId");

                    b.ToTable("UserFavProd", (string)null);
                });

            modelBuilder.Entity("UserHistoryProd", b =>
                {
                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.Property<int>("ProdId")
                        .HasColumnType("int");

                    b.HasKey("UserID", "ProdId")
                        .HasName("PK__UserHist__57CAB4F28EC6E229");

                    b.HasIndex(new[] { "ProdId" }, "IX_UserHistoryProd_ProdId");

                    b.ToTable("UserHistoryProd", (string)null);
                });

            modelBuilder.Entity("DataAccess.Models.Brand", b =>
                {
                    b.HasOne("DataAccess.Models.Category", "Category")
                        .WithMany("Brands")
                        .HasForeignKey("CategoryId")
                        .IsRequired()
                        .HasConstraintName("FK__Brands__Category__06CD04F7");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("DataAccess.Models.PriceHistory", b =>
                {
                    b.HasOne("DataAccess.Models.Product", "Prod")
                        .WithMany("PriceHistories")
                        .HasForeignKey("ProdId")
                        .IsRequired()
                        .HasConstraintName("FK__PriceHist__ProdI__3F466844");

                    b.Navigation("Prod");
                });

            modelBuilder.Entity("DataAccess.Models.Product", b =>
                {
                    b.HasOne("DataAccess.Models.Brand", "Brand")
                        .WithMany("Products")
                        .HasForeignKey("BrandId")
                        .HasConstraintName("FK_Product_Brands");

                    b.HasOne("DataAccess.Models.SubCategory", "SubCategory")
                        .WithMany("Products")
                        .HasForeignKey("SubCategoryId")
                        .IsRequired()
                        .HasConstraintName("FK__Product__SubCate__3C69FB99");

                    b.Navigation("Brand");

                    b.Navigation("SubCategory");
                });

            modelBuilder.Entity("DataAccess.Models.ProductDetail", b =>
                {
                    b.HasOne("DataAccess.Models.ProductLink", "IdNavigation")
                        .WithOne("ProductDetail")
                        .HasForeignKey("DataAccess.Models.ProductDetail", "Id")
                        .IsRequired()
                        .HasConstraintName("FK__ProductDe__Brand__47DBAE45");

                    b.Navigation("IdNavigation");
                });

            modelBuilder.Entity("DataAccess.Models.ProductImage", b =>
                {
                    b.HasOne("DataAccess.Models.Product", "Prod")
                        .WithMany("ProductImages")
                        .HasForeignKey("ProdId")
                        .IsRequired()
                        .HasConstraintName("FK_ProductImages_Product");

                    b.Navigation("Prod");
                });

            modelBuilder.Entity("DataAccess.Models.ProductLink", b =>
                {
                    b.HasOne("DataAccess.Models.Domain", "Domain")
                        .WithMany("ProductLinks")
                        .HasForeignKey("DomainId")
                        .IsRequired()
                        .HasConstraintName("FK__ProductLi__Domai__44FF419A");

                    b.HasOne("DataAccess.Models.Product", "Prod")
                        .WithMany("ProductLinks")
                        .HasForeignKey("ProdId")
                        .IsRequired()
                        .HasConstraintName("FK__ProductLi__ProdI__440B1D61");

                    b.Navigation("Domain");

                    b.Navigation("Prod");
                });

            modelBuilder.Entity("DataAccess.Models.ProductSponsored", b =>
                {
                    b.HasOne("DataAccess.Models.ProductDetail", "ProdDet")
                        .WithMany("ProductSponsoreds")
                        .HasForeignKey("ProdDetId")
                        .IsRequired()
                        .HasConstraintName("FK__ProductSp__ProdI__4D94879B");

                    b.Navigation("ProdDet");
                });

            modelBuilder.Entity("DataAccess.Models.SearchValue", b =>
                {
                    b.HasOne("DataAccess.Models.User", "User")
                        .WithMany("SearchValues")
                        .HasForeignKey("UserID")
                        .IsRequired()
                        .HasConstraintName("FK__SearchVal__UserI__534D60F1");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DataAccess.Models.SubCategory", b =>
                {
                    b.HasOne("DataAccess.Models.Category", "Category")
                        .WithMany("SubCategories")
                        .HasForeignKey("CategoryId")
                        .IsRequired()
                        .HasConstraintName("FK__SubCatego__Categ__398D8EEE");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("DataAccess.Models.User", b =>
                {
                    b.HasOne("DataAccess.Models.AuthUser", "AuthenticatedUser")
                        .WithOne("User")
                        .HasForeignKey("DataAccess.Models.User", "AuthUserID");

                    b.Navigation("AuthenticatedUser");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("DataAccess.Models.AuthUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("DataAccess.Models.AuthUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccess.Models.AuthUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("DataAccess.Models.AuthUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("UserAlertProd", b =>
                {
                    b.HasOne("DataAccess.Models.Product", null)
                        .WithMany()
                        .HasForeignKey("ProdId")
                        .IsRequired()
                        .HasConstraintName("FK__UserAlert__ProdI__5AEE82B9");

                    b.HasOne("DataAccess.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserID")
                        .IsRequired()
                        .HasConstraintName("FK__UserAlert__ProdI__59FA5E80");
                });

            modelBuilder.Entity("UserFavProd", b =>
                {
                    b.HasOne("DataAccess.Models.Product", null)
                        .WithMany()
                        .HasForeignKey("ProdId")
                        .IsRequired()
                        .HasConstraintName("FK__UserFavPr__ProdI__571DF1D5");

                    b.HasOne("DataAccess.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserID")
                        .IsRequired()
                        .HasConstraintName("FK__UserFavPr__ProdI__5629CD9C");
                });

            modelBuilder.Entity("UserHistoryProd", b =>
                {
                    b.HasOne("DataAccess.Models.Product", null)
                        .WithMany()
                        .HasForeignKey("ProdId")
                        .IsRequired()
                        .HasConstraintName("FK__UserHisto__ProdI__5EBF139D");

                    b.HasOne("DataAccess.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserID")
                        .IsRequired()
                        .HasConstraintName("FK__UserHisto__ProdI__5DCAEF64");
                });

            modelBuilder.Entity("DataAccess.Models.AuthUser", b =>
                {
                    b.Navigation("User");
                });

            modelBuilder.Entity("DataAccess.Models.Brand", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("DataAccess.Models.Category", b =>
                {
                    b.Navigation("Brands");

                    b.Navigation("SubCategories");
                });

            modelBuilder.Entity("DataAccess.Models.Domain", b =>
                {
                    b.Navigation("ProductLinks");
                });

            modelBuilder.Entity("DataAccess.Models.Product", b =>
                {
                    b.Navigation("PriceHistories");

                    b.Navigation("ProductImages");

                    b.Navigation("ProductLinks");
                });

            modelBuilder.Entity("DataAccess.Models.ProductDetail", b =>
                {
                    b.Navigation("ProductSponsoreds");
                });

            modelBuilder.Entity("DataAccess.Models.ProductLink", b =>
                {
                    b.Navigation("ProductDetail");
                });

            modelBuilder.Entity("DataAccess.Models.SubCategory", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("DataAccess.Models.User", b =>
                {
                    b.Navigation("SearchValues");
                });
#pragma warning restore 612, 618
        }
    }
}
