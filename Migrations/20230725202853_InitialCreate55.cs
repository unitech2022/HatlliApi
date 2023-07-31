using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HattliApi.Migrations
{
    public partial class InitialCreate55 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MarketId",
                table: "Rates",
                newName: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Rates",
                newName: "MarketId");
        }
    }
}
