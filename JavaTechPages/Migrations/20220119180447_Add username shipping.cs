using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JavaTechPages.Migrations
{
    public partial class Addusernameshipping : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Shippings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Shippings");
        }
    }
}
