using Microsoft.EntityFrameworkCore.Migrations;

namespace GuardaCultura.Migrations
{
    public partial class Utilizadores : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pessoa_Funcao_FuncaoId",
                table: "Pessoa");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Pessoa");

            migrationBuilder.AlterColumn<int>(
                name: "FuncaoId",
                table: "Pessoa",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Pessoa_Funcao_FuncaoId",
                table: "Pessoa",
                column: "FuncaoId",
                principalTable: "Funcao",
                principalColumn: "FuncaoId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pessoa_Funcao_FuncaoId",
                table: "Pessoa");

            migrationBuilder.AlterColumn<int>(
                name: "FuncaoId",
                table: "Pessoa",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Pessoa",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Pessoa_Funcao_FuncaoId",
                table: "Pessoa",
                column: "FuncaoId",
                principalTable: "Funcao",
                principalColumn: "FuncaoId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
