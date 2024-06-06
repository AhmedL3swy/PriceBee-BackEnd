using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class init14 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_AuthUserID",
                table: "Users");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2768ce30-6290-4116-88d8-a92a31873b15");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9087b4e1-5994-476e-8309-e02ce6ce1e8e");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6644d8a3-d259-4c11-9fa9-ffcd8daffac7", "bc52213f-1571-4976-af3e-af8d8e7b37cd", "User", "USER" },
                    { "d0190dec-c36a-475c-8e3b-7c2efb546ad6", "49e0910a-4a61-43b6-96d4-a7ae73e686ca", "Admin", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_AuthUserID",
                table: "Users",
                column: "AuthUserID",
                unique: true,
                filter: "[AuthUserID] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_AuthUserID",
                table: "Users");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6644d8a3-d259-4c11-9fa9-ffcd8daffac7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d0190dec-c36a-475c-8e3b-7c2efb546ad6");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2768ce30-6290-4116-88d8-a92a31873b15", "1fe0dee8-dbfa-4d39-aaed-5d9b06be0157", "Admin", "ADMIN" },
                    { "9087b4e1-5994-476e-8309-e02ce6ce1e8e", "695c2de7-4c87-4b68-afcd-a378026eddf2", "User", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_AuthUserID",
                table: "Users",
                column: "AuthUserID");
        }
    }
}
