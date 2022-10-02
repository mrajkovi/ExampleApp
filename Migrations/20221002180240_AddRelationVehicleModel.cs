using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExampleApp.Migrations
{
    public partial class AddRelationVehicleModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_VehicleModel_MakeId",
                table: "VehicleModel",
                column: "MakeId");

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleModel_VehicleMake_MakeId",
                table: "VehicleModel",
                column: "MakeId",
                principalTable: "VehicleMake",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VehicleModel_VehicleMake_MakeId",
                table: "VehicleModel");

            migrationBuilder.DropIndex(
                name: "IX_VehicleModel_MakeId",
                table: "VehicleModel");
        }
    }
}
