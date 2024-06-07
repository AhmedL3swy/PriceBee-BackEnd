using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class init13 : Migration
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
                keyValue: "b63d13a0-81a0-4926-bc93-425b1f421b47");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b7a2593f-1805-4f0f-9cca-d13d89603263");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "JoinDate",
                table: "AspNetUsers");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<DateOnly>(
                name: "DateOfBirth",
                table: "AspNetUsers",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<DateOnly>(
                name: "JoinDate",
                table: "AspNetUsers",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "b63d13a0-81a0-4926-bc93-425b1f421b47", "3a543441-3330-4f7c-91b8-3986134cf4d6", "User", "USER" },
                    { "b7a2593f-1805-4f0f-9cca-d13d89603263", "23984469-a0e2-410a-aebd-28259379c814", "Admin", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_AuthUserID",
                table: "Users",
                column: "AuthUserID",
                unique: true,
                filter: "[AuthUserID] IS NOT NULL");
        }
    }
}
