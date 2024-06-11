using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name_Local = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Name_Global = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Category__3214EC0750830B99", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Domain",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name_Local = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Name_Global = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Description_Local = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description_Global = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Logo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Domain__3214EC0710D1138D", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductUser",
                columns: table => new
                {
                    ProdId = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductUser", x => new { x.ProdId, x.UserID });
                });

            migrationBuilder.CreateTable(
                name: "ProductUser1",
                columns: table => new
                {
                    ProdId = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductUser1", x => new { x.ProdId, x.UserID });
                });

            migrationBuilder.CreateTable(
                name: "ProductUser2",
                columns: table => new
                {
                    ProdId = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductUser2", x => new { x.ProdId, x.UserID });
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    LName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Country = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    JoinDate = table.Column<DateOnly>(type: "date", nullable: false),
                    PhoneCode = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Users__3214EC07DA5FEBAD", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name_Local = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Name_Global = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Description_Local = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description_Global = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    Logo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Brands__3214EC070B848796", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Brands__Category__06CD04F7",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SubCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name_Local = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Name_Global = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__SubCateg__3214EC07F20DA768", x => x.Id);
                    table.ForeignKey(
                        name: "FK__SubCatego__Categ__398D8EEE",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SearchValues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    SearchValue = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__SearchVa__3214EC075A6F0F8F", x => x.Id);
                    table.ForeignKey(
                        name: "FK__SearchVal__UserI__534D60F1",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name_Local = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Name_Global = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Description_Local = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description_Global = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    SubCategoryId = table.Column<int>(type: "int", nullable: false),
                    BrandId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Product__3214EC073BC2FE02", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_Brands",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__Product__SubCate__3C69FB99",
                        column: x => x.SubCategoryId,
                        principalTable: "SubCategory",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PriceHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProdId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PriceHis__3214EC0755FCD22C", x => x.Id);
                    table.ForeignKey(
                        name: "FK__PriceHist__ProdI__3F466844",
                        column: x => x.ProdId,
                        principalTable: "Product",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProdId = table.Column<int>(type: "int", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ProductI__3214EC075506E1BC", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductImages_Product",
                        column: x => x.ProdId,
                        principalTable: "Product",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductLinks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProdId = table.Column<int>(type: "int", nullable: false),
                    DomainId = table.Column<int>(type: "int", nullable: false),
                    ProductLink = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime", nullable: false),
                    LastScraped = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ProductL__3214EC07EA19D01C", x => x.Id);
                    table.ForeignKey(
                        name: "FK__ProductLi__Domai__44FF419A",
                        column: x => x.DomainId,
                        principalTable: "Domain",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__ProductLi__ProdI__440B1D61",
                        column: x => x.ProdId,
                        principalTable: "Product",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserAlertProd",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false),
                    ProdId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__UserAler__57CAB4F28267EEDA", x => new { x.UserID, x.ProdId });
                    table.ForeignKey(
                        name: "FK__UserAlert__ProdI__59FA5E80",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__UserAlert__ProdI__5AEE82B9",
                        column: x => x.ProdId,
                        principalTable: "Product",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserFavProd",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false),
                    ProdId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__UserFavP__57CAB4F256C39848", x => new { x.UserID, x.ProdId });
                    table.ForeignKey(
                        name: "FK__UserFavPr__ProdI__5629CD9C",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__UserFavPr__ProdI__571DF1D5",
                        column: x => x.ProdId,
                        principalTable: "Product",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserHistoryProd",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false),
                    ProdId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__UserHist__57CAB4F28EC6E229", x => new { x.UserID, x.ProdId });
                    table.ForeignKey(
                        name: "FK__UserHisto__ProdI__5DCAEF64",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__UserHisto__ProdI__5EBF139D",
                        column: x => x.ProdId,
                        principalTable: "Product",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name_Local = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Name_Global = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description_Local = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description_Global = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Rating = table.Column<decimal>(type: "decimal(3,2)", nullable: true),
                    isAvailable = table.Column<bool>(type: "bit", nullable: false),
                    Brand = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ProductD__3214EC07F47B383F", x => x.Id);
                    table.ForeignKey(
                        name: "FK__ProductDe__Brand__47DBAE45",
                        column: x => x.Id,
                        principalTable: "ProductLinks",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductSponsored",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    ProdId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ProductS__3214EC0736B50124", x => x.Id);
                    table.ForeignKey(
                        name: "FK__ProductSp__ProdI__4D94879B",
                        column: x => x.ProdId,
                        principalTable: "ProductDetails",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Brands_CategoryId",
                table: "Brands",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PriceHistory_ProdId",
                table: "PriceHistory",
                column: "ProdId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_BrandId",
                table: "Product",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_SubCategoryId",
                table: "Product",
                column: "SubCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProdId",
                table: "ProductImages",
                column: "ProdId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductLinks_DomainId",
                table: "ProductLinks",
                column: "DomainId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductLinks_ProdId",
                table: "ProductLinks",
                column: "ProdId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSponsored_ProdId",
                table: "ProductSponsored",
                column: "ProdId");

            migrationBuilder.CreateIndex(
                name: "IX_SearchValues_UserID",
                table: "SearchValues",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_SubCategory_CategoryId",
                table: "SubCategory",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAlertProd_ProdId",
                table: "UserAlertProd",
                column: "ProdId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFavProd_ProdId",
                table: "UserFavProd",
                column: "ProdId");

            migrationBuilder.CreateIndex(
                name: "IX_UserHistoryProd_ProdId",
                table: "UserHistoryProd",
                column: "ProdId");

            migrationBuilder.CreateIndex(
                name: "UQ__Users__A9D10534A011E897",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PriceHistory");

            migrationBuilder.DropTable(
                name: "ProductImages");

            migrationBuilder.DropTable(
                name: "ProductSponsored");

            migrationBuilder.DropTable(
                name: "ProductUser");

            migrationBuilder.DropTable(
                name: "ProductUser1");

            migrationBuilder.DropTable(
                name: "ProductUser2");

            migrationBuilder.DropTable(
                name: "SearchValues");

            migrationBuilder.DropTable(
                name: "UserAlertProd");

            migrationBuilder.DropTable(
                name: "UserFavProd");

            migrationBuilder.DropTable(
                name: "UserHistoryProd");

            migrationBuilder.DropTable(
                name: "ProductDetails");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "ProductLinks");

            migrationBuilder.DropTable(
                name: "Domain");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropTable(
                name: "SubCategory");

            migrationBuilder.DropTable(
                name: "Category");
        }
    }
}
