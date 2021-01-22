using Microsoft.EntityFrameworkCore.Migrations;

namespace GuardaCultura.Migrations
{
    public partial class Coordenadas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Coordenadas_gps",
                table: "Miradouro");

            migrationBuilder.AddColumn<bool>(
                name: "Disponibilidade",
                table: "Miradouro",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Latitude_DD",
                table: "Miradouro",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Latitude_DMS",
                table: "Miradouro",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Longitude_DD",
                table: "Miradouro",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Longitude_DMS",
                table: "Miradouro",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Disponibilidade",
                table: "Miradouro");

            migrationBuilder.DropColumn(
                name: "Latitude_DD",
                table: "Miradouro");

            migrationBuilder.DropColumn(
                name: "Latitude_DMS",
                table: "Miradouro");

            migrationBuilder.DropColumn(
                name: "Longitude_DD",
                table: "Miradouro");

            migrationBuilder.DropColumn(
                name: "Longitude_DMS",
                table: "Miradouro");

            migrationBuilder.AddColumn<string>(
                name: "Coordenadas_gps",
                table: "Miradouro",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
