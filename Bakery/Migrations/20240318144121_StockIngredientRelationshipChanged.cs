using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bakery.Migrations
{
    /// <inheritdoc />
    public partial class StockIngredientRelationshipChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IngredientStock");

            migrationBuilder.AddColumn<int>(
                name: "IngredientId",
                table: "Stock",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Stock_IngredientId",
                table: "Stock",
                column: "IngredientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stock_Ingredients_IngredientId",
                table: "Stock",
                column: "IngredientId",
                principalTable: "Ingredients",
                principalColumn: "IngredientId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stock_Ingredients_IngredientId",
                table: "Stock");

            migrationBuilder.DropIndex(
                name: "IX_Stock_IngredientId",
                table: "Stock");

            migrationBuilder.DropColumn(
                name: "IngredientId",
                table: "Stock");

            migrationBuilder.CreateTable(
                name: "IngredientStock",
                columns: table => new
                {
                    IngredientId = table.Column<int>(type: "int", nullable: false),
                    StockId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientStock", x => new { x.IngredientId, x.StockId });
                    table.ForeignKey(
                        name: "FK_IngredientStock_Ingredients_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredients",
                        principalColumn: "IngredientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IngredientStock_Stock_StockId",
                        column: x => x.StockId,
                        principalTable: "Stock",
                        principalColumn: "StockId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IngredientStock_StockId",
                table: "IngredientStock",
                column: "StockId");
        }
    }
}
