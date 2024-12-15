using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HerkansingA2D1.Migrations
{
    /// <inheritdoc />
    public partial class OrderUpdate3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "PromotionPrice",
                table: "OrderItems",
                type: "decimal(18,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PromotionPrice",
                table: "OrderItems");
        }
    }
}
