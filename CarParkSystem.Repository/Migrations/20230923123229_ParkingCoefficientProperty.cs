using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarParkSystem.Repository.Migrations
{
    /// <inheritdoc />
    public partial class ParkingCoefficientProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParkingCoefficient",
                table: "SecondClassVehicleFeatures",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ParkingCoefficient",
                table: "FirstClassVehicleFeatures",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParkingCoefficient",
                table: "SecondClassVehicleFeatures");

            migrationBuilder.DropColumn(
                name: "ParkingCoefficient",
                table: "FirstClassVehicleFeatures");
        }
    }
}
