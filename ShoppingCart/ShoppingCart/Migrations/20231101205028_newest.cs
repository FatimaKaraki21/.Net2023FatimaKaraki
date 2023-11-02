using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoppingCart.Migrations
{
    public partial class newest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItem_Orders_OrderId",
                table: "CartItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CartItem",
                table: "CartItem");

            migrationBuilder.RenameTable(
                name: "CartItem",
                newName: "cartItems");

            migrationBuilder.RenameIndex(
                name: "IX_CartItem_OrderId",
                table: "cartItems",
                newName: "IX_cartItems_OrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_cartItems",
                table: "cartItems",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "admin-role",
                column: "ConcurrencyStamp",
                value: "e8df221c-3450-4dad-8093-69da8f4e91aa");

            migrationBuilder.AddForeignKey(
                name: "FK_cartItems_Orders_OrderId",
                table: "cartItems",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cartItems_Orders_OrderId",
                table: "cartItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_cartItems",
                table: "cartItems");

            migrationBuilder.RenameTable(
                name: "cartItems",
                newName: "CartItem");

            migrationBuilder.RenameIndex(
                name: "IX_cartItems_OrderId",
                table: "CartItem",
                newName: "IX_CartItem_OrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CartItem",
                table: "CartItem",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "admin-role",
                column: "ConcurrencyStamp",
                value: "dc4093a6-f167-48c4-9651-8626799a27bc");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItem_Orders_OrderId",
                table: "CartItem",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");
        }
    }
}
