using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class init11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "47e2a351-1569-4101-afcf-dbd12c9c8c3d", "c4279b0d-15ba-4ba6-82c7-f99a1d408fb7", "Admin", "ADMIN" },
                    { "61b57e44-3048-4ee0-bb6f-21382efb7a79", "e08366e0-cfc5-4844-ad74-fa773983fb5b", "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "47e2a351-1569-4101-afcf-dbd12c9c8c3d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "61b57e44-3048-4ee0-bb6f-21382efb7a79");
        }
    }
}
