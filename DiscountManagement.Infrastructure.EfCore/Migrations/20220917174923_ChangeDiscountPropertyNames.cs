using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiscountManagement.Infrastructure.EfCore.Migrations
{
    public partial class ChangeDiscountPropertyNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Percent",
                table: "OrderDiscounts",
                newName: "DiscountRate");

            migrationBuilder.RenameColumn(
                name: "Code",
                table: "OrderDiscounts",
                newName: "DiscountCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DiscountRate",
                table: "OrderDiscounts",
                newName: "Percent");

            migrationBuilder.RenameColumn(
                name: "DiscountCode",
                table: "OrderDiscounts",
                newName: "Code");
        }
    }
}
