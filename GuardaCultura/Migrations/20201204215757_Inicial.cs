using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GuardaCultura.Migrations
{
    public partial class Inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Duracao",
                columns: table => new
                {
                    DuracaoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HorasInicio = table.Column<int>(maxLength: 2, nullable: false),
                    HorasFim = table.Column<int>(maxLength: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Duracao", x => x.DuracaoId);
                });

            migrationBuilder.CreateTable(
                name: "EstacaoAno",
                columns: table => new
                {
                    EstacaoAnoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome_estacao = table.Column<string>(maxLength: 9, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstacaoAno", x => x.EstacaoAnoId);
                });

            migrationBuilder.CreateTable(
                name: "Hora",
                columns: table => new
                {
                    HoraId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Horas = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hora", x => x.HoraId);
                });

            migrationBuilder.CreateTable(
                name: "Miradouro",
                columns: table => new
                {
                    MiradouroId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(maxLength: 256, nullable: false),
                    Localizacao = table.Column<string>(maxLength: 256, nullable: false),
                    Coordenadas_gps = table.Column<string>(nullable: false),
                    Terreno = table.Column<string>(nullable: false),
                    E_Miradouro = table.Column<bool>(nullable: false),
                    Condicoes = table.Column<string>(nullable: true),
                    Ocupacao_maxima = table.Column<int>(nullable: false),
                    Descricao = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Miradouro", x => x.MiradouroId);
                });

            migrationBuilder.CreateTable(
                name: "TipoImagem",
                columns: table => new
                {
                    TipoImagemId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoImagem", x => x.TipoImagemId);
                });

            migrationBuilder.CreateTable(
                name: "Atratividade",
                columns: table => new
                {
                    AtratividadeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DuracaoId = table.Column<int>(nullable: false),
                    EstacaoAnoId = table.Column<int>(nullable: false),
                    MiradouroId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Atratividade", x => x.AtratividadeId);
                    table.ForeignKey(
                        name: "FK_Atratividade_Duracao_DuracaoId",
                        column: x => x.DuracaoId,
                        principalTable: "Duracao",
                        principalColumn: "DuracaoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Atratividade_EstacaoAno_EstacaoAnoId",
                        column: x => x.EstacaoAnoId,
                        principalTable: "EstacaoAno",
                        principalColumn: "EstacaoAnoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Atratividade_Miradouro_MiradouroId",
                        column: x => x.MiradouroId,
                        principalTable: "Miradouro",
                        principalColumn: "MiradouroId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ocupacao",
                columns: table => new
                {
                    OcupacaoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Numero_pessoas = table.Column<int>(nullable: false),
                    Data = table.Column<string>(nullable: false),
                    MiradouroId = table.Column<int>(nullable: false),
                    HoraId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ocupacao", x => x.OcupacaoId);
                    table.ForeignKey(
                        name: "FK_Ocupacao_Hora_HoraId",
                        column: x => x.HoraId,
                        principalTable: "Hora",
                        principalColumn: "HoraId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ocupacao_Miradouro_MiradouroId",
                        column: x => x.MiradouroId,
                        principalTable: "Miradouro",
                        principalColumn: "MiradouroId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Fotografia",
                columns: table => new
                {
                    FotografiaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(nullable: false),
                    Data_imagem = table.Column<string>(nullable: true),
                    Classificacao = table.Column<float>(nullable: false),
                    Foto = table.Column<byte[]>(nullable: true),
                    EstacaoAnoId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    MiradouroId = table.Column<int>(nullable: false),
                    TipoImagemId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fotografia", x => x.FotografiaId);
                    table.ForeignKey(
                        name: "FK_Fotografia_EstacaoAno_EstacaoAnoId",
                        column: x => x.EstacaoAnoId,
                        principalTable: "EstacaoAno",
                        principalColumn: "EstacaoAnoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Fotografia_Miradouro_MiradouroId",
                        column: x => x.MiradouroId,
                        principalTable: "Miradouro",
                        principalColumn: "MiradouroId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Fotografia_TipoImagem_TipoImagemId",
                        column: x => x.TipoImagemId,
                        principalTable: "TipoImagem",
                        principalColumn: "TipoImagemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Atratividade_DuracaoId",
                table: "Atratividade",
                column: "DuracaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Atratividade_EstacaoAnoId",
                table: "Atratividade",
                column: "EstacaoAnoId");

            migrationBuilder.CreateIndex(
                name: "IX_Atratividade_MiradouroId",
                table: "Atratividade",
                column: "MiradouroId");

            migrationBuilder.CreateIndex(
                name: "IX_Fotografia_EstacaoAnoId",
                table: "Fotografia",
                column: "EstacaoAnoId");

            migrationBuilder.CreateIndex(
                name: "IX_Fotografia_MiradouroId",
                table: "Fotografia",
                column: "MiradouroId");

            migrationBuilder.CreateIndex(
                name: "IX_Fotografia_TipoImagemId",
                table: "Fotografia",
                column: "TipoImagemId");

            migrationBuilder.CreateIndex(
                name: "IX_Ocupacao_HoraId",
                table: "Ocupacao",
                column: "HoraId");

            migrationBuilder.CreateIndex(
                name: "IX_Ocupacao_MiradouroId",
                table: "Ocupacao",
                column: "MiradouroId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Atratividade");

            migrationBuilder.DropTable(
                name: "Fotografia");

            migrationBuilder.DropTable(
                name: "Ocupacao");

            migrationBuilder.DropTable(
                name: "Duracao");

            migrationBuilder.DropTable(
                name: "EstacaoAno");

            migrationBuilder.DropTable(
                name: "TipoImagem");

            migrationBuilder.DropTable(
                name: "Hora");

            migrationBuilder.DropTable(
                name: "Miradouro");
        }
    }
}
