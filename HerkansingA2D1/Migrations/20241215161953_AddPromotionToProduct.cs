using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HerkansingA2D1.Migrations
{
    /// <inheritdoc />
    public partial class AddPromotionToProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "PromotionEnd",
                table: "Product",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PromotionStart",
                table: "Product",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PromotionalPrice",
                table: "Product",
                type: "decimal(18,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PromotionEnd",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "PromotionStart",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "PromotionalPrice",
                table: "Product");
        }
    }
}
