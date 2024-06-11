using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class init17 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "71381593-5d85-46ef-a994-81dfde21916b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "95b0379a-74d4-4e60-b8b8-b215005524fe");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5d1f3368-342a-49a9-8dac-db1c2a140e5c", "1bf99acc-bb3b-4931-95f5-e2c58b3ef859", "Admin", "ADMIN" },
                    { "5e950883-a632-460a-8b3d-399a678d8282", "3442a882-25bb-4854-ba0f-8070b357e24e", "User", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "UQ__Users__A9D10534A011E897",
                table: "AspNetUsers",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UQ__Users__A9D10534A011E897",
                table: "AspNetUsers");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5d1f3368-342a-49a9-8dac-db1c2a140e5c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5e950883-a632-460a-8b3d-399a678d8282");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "71381593-5d85-46ef-a994-81dfde21916b", "c93116c7-56b4-456f-85f0-8c44752d8881", "User", "USER" },
                    { "95b0379a-74d4-4e60-b8b8-b215005524fe", "a30bf196-4fb6-4a1c-b412-e4e5be982423", "Admin", "ADMIN" }
                });
        }
    }
}
