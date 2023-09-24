using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarParkSystem.Repository.Migrations
{
    /// <inheritdoc />
    public partial class CarWashFeeAndTireChangeFee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "CarWashFee",
                table: "CarPark",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TireChangeFee",
                table: "CarPark",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CarWashFee",
                table: "CarPark");

            migrationBuilder.DropColumn(
                name: "TireChangeFee",
                table: "CarPark");
        }
    }
}
