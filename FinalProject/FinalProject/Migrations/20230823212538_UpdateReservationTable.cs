using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalProject.Migrations
{
    public partial class UpdateReservationTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StarterMenuImages_Desserts_DessertId",
                table: "StarterMenuImages");

            migrationBuilder.DropIndex(
                name: "IX_StarterMenuImages_DessertId",
                table: "StarterMenuImages");

            migrationBuilder.DropColumn(
                name: "DessertId",
                table: "StarterMenuImages");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DessertId",
                table: "StarterMenuImages",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StarterMenuImages_DessertId",
                table: "StarterMenuImages",
                column: "DessertId");

            migrationBuilder.AddForeignKey(
                name: "FK_StarterMenuImages_Desserts_DessertId",
                table: "StarterMenuImages",
                column: "DessertId",
                principalTable: "Desserts",
                principalColumn: "Id");
        }
    }
}
