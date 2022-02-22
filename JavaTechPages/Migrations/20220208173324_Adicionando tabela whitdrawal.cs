using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JavaTechPages.Migrations
{
    public partial class Adicionandotabelawhitdrawal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Whitdrawals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExitDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    QuantityExit = table.Column<int>(type: "int", nullable: false),
                    ProductUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Whitdrawals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Whitdrawals_productUsers_ProductUserId",
                        column: x => x.ProductUserId,
                        principalTable: "productUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Whitdrawals_ProductUserId",
                table: "Whitdrawals",
                column: "ProductUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Whitdrawals");
        }
    }
}
