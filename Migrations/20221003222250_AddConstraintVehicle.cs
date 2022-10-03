using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExampleApp.Migrations
{
    public partial class AddConstraintVehicle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_VehicleModel_Name",
                table: "VehicleModel",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VehicleMake_Name",
                table: "VehicleMake",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_VehicleModel_Name",
                table: "VehicleModel");

            migrationBuilder.DropIndex(
                name: "IX_VehicleMake_Name",
                table: "VehicleMake");
        }
    }
}
