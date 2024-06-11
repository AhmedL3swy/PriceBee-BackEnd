using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class init15 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6644d8a3-d259-4c11-9fa9-ffcd8daffac7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d0190dec-c36a-475c-8e3b-7c2efb546ad6");

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
                    { "cc25cf0a-9886-4ac2-b8c0-e5e74a24e373", "c04acf10-6ac9-4936-9237-50366fb9400b", "User", "USER" },
                    { "d642cb2b-ffc6-4f82-b4cf-b34639b620e2", "7a20faa8-6989-4658-8b88-5346d83215b4", "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cc25cf0a-9886-4ac2-b8c0-e5e74a24e373");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d642cb2b-ffc6-4f82-b4cf-b34639b620e2");

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
                    { "6644d8a3-d259-4c11-9fa9-ffcd8daffac7", "bc52213f-1571-4976-af3e-af8d8e7b37cd", "User", "USER" },
                    { "d0190dec-c36a-475c-8e3b-7c2efb546ad6", "49e0910a-4a61-43b6-96d4-a7ae73e686ca", "Admin", "ADMIN" }
                });
        }
    }
}
