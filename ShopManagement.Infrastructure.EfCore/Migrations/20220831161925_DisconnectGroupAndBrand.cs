using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopManagement.Infrastructure.EfCore.Migrations
{
    public partial class DisconnectGroupAndBrand : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductBrands_ProductGroups_GroupId",
                table: "ProductBrands");

            migrationBuilder.DropIndex(
                name: "IX_ProductBrands_GroupId",
                table: "ProductBrands");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "ProductBrands");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "GroupId",
                table: "ProductBrands",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_ProductBrands_GroupId",
                table: "ProductBrands",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductBrands_ProductGroups_GroupId",
                table: "ProductBrands",
                column: "GroupId",
                principalTable: "ProductGroups",
                principalColumn: "GroupId");
        }
    }
}
