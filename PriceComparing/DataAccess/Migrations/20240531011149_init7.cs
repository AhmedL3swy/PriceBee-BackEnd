using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class init7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_AspNetUsers_AuthenticatedUserId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_AuthenticatedUserId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "AuthenticatedUserId",
                table: "Users",
                newName: "AuthUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_AuthUserID",
                table: "Users",
                column: "AuthUserID",
                unique: true,
                filter: "[AuthUserID] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_AspNetUsers_AuthUserID",
                table: "Users",
                column: "AuthUserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_AspNetUsers_AuthUserID",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_AuthUserID",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "AuthUserID",
                table: "Users",
                newName: "AuthenticatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_AuthenticatedUserId",
                table: "Users",
                column: "AuthenticatedUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_AspNetUsers_AuthenticatedUserId",
                table: "Users",
                column: "AuthenticatedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
