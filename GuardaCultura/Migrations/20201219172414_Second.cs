using Microsoft.EntityFrameworkCore.Migrations;

namespace GuardaCultura.Migrations
{
    public partial class Second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "Miradouro",
                nullable: false);

            migrationBuilder.AddColumn<bool>(
                name: "Aprovada",
                table: "Fotografia",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "Miradouro");

            migrationBuilder.DropColumn(
                name: "Aprovada",
                table: "Fotografia");
        }
    }
}
