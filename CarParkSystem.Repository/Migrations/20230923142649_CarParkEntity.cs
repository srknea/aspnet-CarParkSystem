using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarParkSystem.Repository.Migrations
{
    /// <inheritdoc />
    public partial class CarParkEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CarPark",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsOpen = table.Column<bool>(type: "bit", nullable: false),
                    HourlyRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarPark", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CarParkCategory",
                columns: table => new
                {
                    CarParksId = table.Column<int>(type: "int", nullable: false),
                    CategoriesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarParkCategory", x => new { x.CarParksId, x.CategoriesId });
                    table.ForeignKey(
                        name: "FK_CarParkCategory_CarPark_CarParksId",
                        column: x => x.CarParksId,
                        principalTable: "CarPark",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarParkCategory_Categories_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarParkCategory_CategoriesId",
                table: "CarParkCategory",
                column: "CategoriesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarParkCategory");

            migrationBuilder.DropTable(
                name: "CarPark");
        }
    }
}
