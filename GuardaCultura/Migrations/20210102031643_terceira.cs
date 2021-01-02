using Microsoft.EntityFrameworkCore.Migrations;

namespace GuardaCultura.Migrations
{
    public partial class terceira : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "N_Votos",
                table: "Fotografia",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "N_Votos",
                table: "Fotografia");
        }
    }
}
