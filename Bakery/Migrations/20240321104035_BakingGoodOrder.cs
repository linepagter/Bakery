using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bakery.Migrations
{
    /// <inheritdoc />
    public partial class BakingGoodOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Listofbakinggoods");

            migrationBuilder.CreateTable(
                name: "Bakinggood",
                columns: table => new
                {
                    BakingGoodId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bakinggood", x => x.BakingGoodId);
                });

            migrationBuilder.CreateTable(
                name: "BakingGoodOrders",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    BakingGoodId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BakingGoodOrders", x => new { x.OrderId, x.BakingGoodId });
                    table.ForeignKey(
                        name: "FK_BakingGoodOrders_Bakinggood_BakingGoodId",
                        column: x => x.BakingGoodId,
                        principalTable: "Bakinggood",
                        principalColumn: "BakingGoodId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BakingGoodOrders_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BakingGoodOrders_BakingGoodId",
                table: "BakingGoodOrders",
                column: "BakingGoodId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BakingGoodOrders");

            migrationBuilder.DropTable(
                name: "Bakinggood");

            migrationBuilder.CreateTable(
                name: "Listofbakinggoods",
                columns: table => new
                {
                    ListId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrdreId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Listofbakinggoods", x => x.ListId);
                    table.ForeignKey(
                        name: "FK_Listofbakinggoods_Order_OrdreId",
                        column: x => x.OrdreId,
                        principalTable: "Order",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Listofbakinggoods_OrdreId",
                table: "Listofbakinggoods",
                column: "OrdreId",
                unique: true);
        }
    }
}
