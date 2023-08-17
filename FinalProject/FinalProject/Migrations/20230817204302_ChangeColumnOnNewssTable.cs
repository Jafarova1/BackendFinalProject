using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalProject.Migrations
{
    public partial class ChangeColumnOnNewssTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Newss",
                newName: "SecondTitle");

            migrationBuilder.AddColumn<string>(
                name: "FirstTitle",
                table: "Newss",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstTitle",
                table: "Newss");

            migrationBuilder.RenameColumn(
                name: "SecondTitle",
                table: "Newss",
                newName: "Title");
        }
    }
}
