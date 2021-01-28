using Microsoft.EntityFrameworkCore.Migrations;

namespace GuardaCultura.Migrations
{
    public partial class AtualizarPessoa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Bloqueio",
                table: "Pessoa",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bloqueio",
                table: "Pessoa");
        }
    }
}
