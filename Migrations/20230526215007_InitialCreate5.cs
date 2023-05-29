using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HattliApi.Migrations
{
    public partial class InitialCreate5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "status",
                table: "Products",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "rate",
                table: "Products",
                newName: "Rate");

            migrationBuilder.RenameColumn(
                name: "providerId",
                table: "Products",
                newName: "ProviderId");

            migrationBuilder.RenameColumn(
                name: "price",
                table: "Products",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Products",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "images",
                table: "Products",
                newName: "Images");

            migrationBuilder.RenameColumn(
                name: "discount",
                table: "Products",
                newName: "Discount");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "Products",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "createdAt",
                table: "Products",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "categoryId",
                table: "Products",
                newName: "CategoryId");

            migrationBuilder.RenameColumn(
                name: "calories",
                table: "Products",
                newName: "Calories");

            migrationBuilder.RenameColumn(
                name: "brandID",
                table: "Products",
                newName: "BrandID");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Products",
                newName: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Products",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "Rate",
                table: "Products",
                newName: "rate");

            migrationBuilder.RenameColumn(
                name: "ProviderId",
                table: "Products",
                newName: "providerId");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Products",
                newName: "price");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Products",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Images",
                table: "Products",
                newName: "images");

            migrationBuilder.RenameColumn(
                name: "Discount",
                table: "Products",
                newName: "discount");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Products",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Products",
                newName: "createdAt");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Products",
                newName: "categoryId");

            migrationBuilder.RenameColumn(
                name: "Calories",
                table: "Products",
                newName: "calories");

            migrationBuilder.RenameColumn(
                name: "BrandID",
                table: "Products",
                newName: "brandID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Products",
                newName: "id");
        }
    }
}
