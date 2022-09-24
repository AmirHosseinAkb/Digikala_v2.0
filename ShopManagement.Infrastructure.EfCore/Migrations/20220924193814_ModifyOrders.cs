using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopManagement.Infrastructure.EfCore.Migrations
{
    public partial class ModifyOrders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_ProductColors_ProductColorColorId1",
                table: "OrderItem");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_Products_ProductId1",
                table: "OrderItem");

            migrationBuilder.DropIndex(
                name: "IX_OrderItem_ProductColorColorId1",
                table: "OrderItem");

            migrationBuilder.DropIndex(
                name: "IX_OrderItem_ProductId1",
                table: "OrderItem");

            migrationBuilder.DropColumn(
                name: "ProductColorColorId1",
                table: "OrderItem");

            migrationBuilder.DropColumn(
                name: "ProductId1",
                table: "OrderItem");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ProductColorColorId1",
                table: "OrderItem",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ProductId1",
                table: "OrderItem",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_ProductColorColorId1",
                table: "OrderItem",
                column: "ProductColorColorId1");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_ProductId1",
                table: "OrderItem",
                column: "ProductId1");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_ProductColors_ProductColorColorId1",
                table: "OrderItem",
                column: "ProductColorColorId1",
                principalTable: "ProductColors",
                principalColumn: "ColorId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_Products_ProductId1",
                table: "OrderItem",
                column: "ProductId1",
                principalTable: "Products",
                principalColumn: "ProductId");
        }
    }
}
