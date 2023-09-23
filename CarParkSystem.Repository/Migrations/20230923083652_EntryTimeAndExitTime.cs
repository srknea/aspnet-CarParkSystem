using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarParkSystem.Repository.Migrations
{
    /// <inheritdoc />
    public partial class EntryTimeAndExitTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "SecondClassVehicleFeatures");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "SecondClassVehicleFeatures");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "FirstClassVehicleFeatures");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "FirstClassVehicleFeatures");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "Categories");

            migrationBuilder.RenameColumn(
                name: "UpdatedDate",
                table: "Vehicles",
                newName: "ExitTime");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Vehicles",
                newName: "EntryTime");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ExitTime",
                table: "Vehicles",
                newName: "UpdatedDate");

            migrationBuilder.RenameColumn(
                name: "EntryTime",
                table: "Vehicles",
                newName: "CreatedDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "SecondClassVehicleFeatures",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "SecondClassVehicleFeatures",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "FirstClassVehicleFeatures",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "FirstClassVehicleFeatures",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Categories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "Categories",
                type: "datetime2",
                nullable: true);
        }
    }
}
