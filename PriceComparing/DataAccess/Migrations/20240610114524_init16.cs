using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class init16 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cc25cf0a-9886-4ac2-b8c0-e5e74a24e373");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d642cb2b-ffc6-4f82-b4cf-b34639b620e2");

            migrationBuilder.RenameColumn(
                name: "ProdId",
                table: "ProductSponsored",
                newName: "ProdDetId");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "SubCategory",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "SearchValues",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ProductSponsored",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ProductLinks",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ProductImages",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ProductDetails",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Product",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "PriceHistory",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Domain",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Category",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Brands",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "71381593-5d85-46ef-a994-81dfde21916b", "c93116c7-56b4-456f-85f0-8c44752d8881", "User", "USER" },
                    { "95b0379a-74d4-4e60-b8b8-b215005524fe", "a30bf196-4fb6-4a1c-b412-e4e5be982423", "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "71381593-5d85-46ef-a994-81dfde21916b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "95b0379a-74d4-4e60-b8b8-b215005524fe");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "SubCategory");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "SearchValues");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ProductSponsored");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ProductLinks");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ProductImages");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ProductDetails");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "PriceHistory");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Domain");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Brands");

            migrationBuilder.RenameColumn(
                name: "ProdDetId",
                table: "ProductSponsored",
                newName: "ProdId");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "cc25cf0a-9886-4ac2-b8c0-e5e74a24e373", "c04acf10-6ac9-4936-9237-50366fb9400b", "User", "USER" },
                    { "d642cb2b-ffc6-4f82-b4cf-b34639b620e2", "7a20faa8-6989-4658-8b88-5346d83215b4", "Admin", "ADMIN" }
                });
        }
    }
}
