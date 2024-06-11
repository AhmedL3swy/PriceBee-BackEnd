using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class init12 : Migration
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
                keyValue: "47e2a351-1569-4101-afcf-dbd12c9c8c3d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "61b57e44-3048-4ee0-bb6f-21382efb7a79");

            migrationBuilder.AddColumn<DateOnly>(
                name: "DateOfBirth",
                table: "AspNetUsers",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "Image",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "JoinDate",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "47e2a351-1569-4101-afcf-dbd12c9c8c3d", "c4279b0d-15ba-4ba6-82c7-f99a1d408fb7", "Admin", "ADMIN" },
                    { "61b57e44-3048-4ee0-bb6f-21382efb7a79", "e08366e0-cfc5-4844-ad74-fa773983fb5b", "User", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_AuthUserID",
                table: "Users",
                column: "AuthUserID");
        }
    }
}
