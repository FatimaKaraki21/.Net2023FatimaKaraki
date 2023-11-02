using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoppingCart.Migrations
{
    public partial class final : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "admin-role",
                column: "ConcurrencyStamp",
                value: "752ab724-b6b9-428e-982b-7cdec96a3255");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "admin-role",
                column: "ConcurrencyStamp",
                value: "e8df221c-3450-4dad-8093-69da8f4e91aa");
        }
    }
}
