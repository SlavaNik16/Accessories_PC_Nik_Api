using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Accessories_PC_Nik.Context.Migrations
{
    public partial class AddNameComponents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Components",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Components");
        }
    }
}
