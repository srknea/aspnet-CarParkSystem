using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarParkSystem.Repository.Migrations
{
    /// <inheritdoc />
    public partial class UpdateForVehicleEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EnginePowerKilowatt",
                table: "Vehicles",
                newName: "Torque");

            migrationBuilder.AddColumn<int>(
                name: "EngineRPM",
                table: "Vehicles",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EngineRPM",
                table: "Vehicles");

            migrationBuilder.RenameColumn(
                name: "Torque",
                table: "Vehicles",
                newName: "EnginePowerKilowatt");
        }
    }
}
