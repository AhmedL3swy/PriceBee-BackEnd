using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class PhoneValid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c3d2def0-eb76-450b-a82a-12bbe0d4e381");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fb3b171e-47ca-42c2-8316-2e7bb17c85fa");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0f82d3d5-28c3-4139-b45c-0401ebdd6bf6", "5a481a35-8aa8-4978-9e97-599e0871707b", "User", "USER" },
                    { "1a18203c-50c7-4b71-b846-9ec006dc26f6", "8be35ab6-508e-4641-9515-173c3242bab4", "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0f82d3d5-28c3-4139-b45c-0401ebdd6bf6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1a18203c-50c7-4b71-b846-9ec006dc26f6");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "c3d2def0-eb76-450b-a82a-12bbe0d4e381", "637a77fa-2d4b-4aea-9486-8b69f6a28e63", "Admin", "ADMIN" },
                    { "fb3b171e-47ca-42c2-8316-2e7bb17c85fa", "3fbab4fb-ad5d-49e1-a989-363b10ffbed0", "User", "USER" }
                });
        }
    }
}
