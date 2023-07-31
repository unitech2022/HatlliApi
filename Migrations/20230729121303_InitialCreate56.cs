using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HattliApi.Migrations
{
    public partial class InitialCreate56 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "OrderWallets",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "OrderWallets");
        }
    }
}
