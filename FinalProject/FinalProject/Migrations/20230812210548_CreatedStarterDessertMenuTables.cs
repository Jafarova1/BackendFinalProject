using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalProject.Migrations
{
    public partial class CreatedStarterDessertMenuTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Desserts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoftDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Desserts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoftDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Starters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoftDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Starters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DessertMenuImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsMain = table.Column<bool>(type: "bit", nullable: false),
                    DessertMenuId = table.Column<int>(type: "int", nullable: false),
                    DessertId = table.Column<int>(type: "int", nullable: false),
                    SoftDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DessertMenuImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DessertMenuImages_Desserts_DessertId",
                        column: x => x.DessertId,
                        principalTable: "Desserts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StarterMenuImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsMain = table.Column<bool>(type: "bit", nullable: false),
                    StarterMenuId = table.Column<int>(type: "int", nullable: false),
                    StarterId = table.Column<int>(type: "int", nullable: false),
                    DessertId = table.Column<int>(type: "int", nullable: true),
                    SoftDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StarterMenuImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StarterMenuImages_Desserts_DessertId",
                        column: x => x.DessertId,
                        principalTable: "Desserts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StarterMenuImages_Starters_StarterId",
                        column: x => x.StarterId,
                        principalTable: "Starters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DessertMenuImages_DessertId",
                table: "DessertMenuImages",
                column: "DessertId");

            migrationBuilder.CreateIndex(
                name: "IX_StarterMenuImages_DessertId",
                table: "StarterMenuImages",
                column: "DessertId");

            migrationBuilder.CreateIndex(
                name: "IX_StarterMenuImages_StarterId",
                table: "StarterMenuImages",
                column: "StarterId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DessertMenuImages");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "StarterMenuImages");

            migrationBuilder.DropTable(
                name: "Desserts");

            migrationBuilder.DropTable(
                name: "Starters");
        }
    }
}
