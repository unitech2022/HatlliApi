using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HattliApi.Migrations
{
    public partial class InitialCreate1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Birth",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "Gender",
                table: "AspNetUsers",
                newName: "TypeService");

            migrationBuilder.AddColumn<double>(
                name: "Address",
                table: "AspNetUsers",
                type: "double",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImagePassport",
                table: "AspNetUsers",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "LogoCompany",
                table: "AspNetUsers",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "NameAdministratorCompany",
                table: "AspNetUsers",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "ProfileImage",
                table: "AspNetUsers",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<double>(
                name: "Rate",
                table: "AspNetUsers",
                type: "double",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ImagePassport",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LogoCompany",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "NameAdministratorCompany",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ProfileImage",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Rate",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "TypeService",
                table: "AspNetUsers",
                newName: "Gender");

            migrationBuilder.AddColumn<DateTime>(
                name: "Birth",
                table: "AspNetUsers",
                type: "datetime(6)",
                nullable: true);
        }
    }
}
