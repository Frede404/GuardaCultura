﻿// <auto-generated />
using System;
using GuardaCultura.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GuardaCultura.Migrations
{
    [DbContext(typeof(GuardaCulturaContext))]
    [Migration("20210102031643_terceira")]
    partial class terceira
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GuardaCultura.Models.Atratividade", b =>
                {
                    b.Property<int>("AtratividadeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DuracaoId")
                        .HasColumnType("int");

                    b.Property<int>("EstacaoAnoId")
                        .HasColumnType("int");

                    b.Property<int>("MiradouroId")
                        .HasColumnType("int");

                    b.HasKey("AtratividadeId");

                    b.HasIndex("DuracaoId");

                    b.HasIndex("EstacaoAnoId");

                    b.HasIndex("MiradouroId");

                    b.ToTable("Atratividade");
                });

            modelBuilder.Entity("GuardaCultura.Models.Duracao", b =>
                {
                    b.Property<int>("DuracaoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("HorasFim")
                        .HasColumnType("int")
                        .HasMaxLength(2);

                    b.Property<int>("HorasInicio")
                        .HasColumnType("int")
                        .HasMaxLength(2);

                    b.HasKey("DuracaoId");

                    b.ToTable("Duracao");
                });

            modelBuilder.Entity("GuardaCultura.Models.EstacaoAno", b =>
                {
                    b.Property<int>("EstacaoAnoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nome_estacao")
                        .IsRequired()
                        .HasColumnType("nvarchar(9)")
                        .HasMaxLength(9);

                    b.HasKey("EstacaoAnoId");

                    b.ToTable("EstacaoAno");
                });

            modelBuilder.Entity("GuardaCultura.Models.Fotografia", b =>
                {
                    b.Property<int>("FotografiaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Aprovada")
                        .HasColumnType("bit");

                    b.Property<float>("Classificacao")
                        .HasColumnType("real");

                    b.Property<string>("Data_imagem")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EstacaoAnoId")
                        .HasColumnType("int");

                    b.Property<byte[]>("Foto")
                        .HasColumnType("varbinary(max)");

                    b.Property<int>("MiradouroId")
                        .HasColumnType("int");

                    b.Property<int>("N_Votos")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PessoaId")
                        .HasColumnType("int");

                    b.Property<int>("TipoImagemId")
                        .HasColumnType("int");

                    b.HasKey("FotografiaId");

                    b.HasIndex("EstacaoAnoId");

                    b.HasIndex("MiradouroId");

                    b.HasIndex("PessoaId");

                    b.HasIndex("TipoImagemId");

                    b.ToTable("Fotografia");
                });

            modelBuilder.Entity("GuardaCultura.Models.Funcao", b =>
                {
                    b.Property<int>("FuncaoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FuncaoDesempenhar")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("FuncaoId");

                    b.ToTable("Funcao");
                });

            modelBuilder.Entity("GuardaCultura.Models.Hora", b =>
                {
                    b.Property<int>("HoraId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Horas")
                        .HasColumnType("int");

                    b.HasKey("HoraId");

                    b.ToTable("Hora");
                });

            modelBuilder.Entity("GuardaCultura.Models.Miradouro", b =>
                {
                    b.Property<int>("MiradouroId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<string>("Condicoes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Coordenadas_gps")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Descricao")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<bool>("E_Miradouro")
                        .HasColumnType("bit");

                    b.Property<string>("Localizacao")
                        .IsRequired()
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<int>("Ocupacao_maxima")
                        .HasColumnType("int");

                    b.Property<string>("Terreno")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MiradouroId");

                    b.ToTable("Miradouro");
                });

            modelBuilder.Entity("GuardaCultura.Models.Ocupacao", b =>
                {
                    b.Property<int>("OcupacaoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("HoraId")
                        .HasColumnType("int");

                    b.Property<int>("MiradouroId")
                        .HasColumnType("int");

                    b.Property<int>("Numero_pessoas")
                        .HasColumnType("int");

                    b.HasKey("OcupacaoId");

                    b.HasIndex("HoraId");

                    b.HasIndex("MiradouroId");

                    b.ToTable("Ocupacao");
                });

            modelBuilder.Entity("GuardaCultura.Models.Pessoa", b =>
                {
                    b.Property<int>("PessoaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Data_Nasc")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(512)")
                        .HasMaxLength(512);

                    b.Property<float>("Fiabilidade")
                        .HasColumnType("real");

                    b.Property<int>("FuncaoId")
                        .HasColumnType("int");

                    b.Property<string>("Nacionalidade")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(512)")
                        .HasMaxLength(512);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.Property<string>("Sexo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ultima_Lingua")
                        .HasColumnType("nvarchar(5)")
                        .HasMaxLength(5);

                    b.HasKey("PessoaId");

                    b.HasIndex("FuncaoId");

                    b.ToTable("Pessoa");
                });

            modelBuilder.Entity("GuardaCultura.Models.TipoImagem", b =>
                {
                    b.Property<int>("TipoImagemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TipoImagemId");

                    b.ToTable("TipoImagem");
                });

            modelBuilder.Entity("GuardaCultura.Models.Atratividade", b =>
                {
                    b.HasOne("GuardaCultura.Models.Duracao", "Duracao")
                        .WithMany("Atratividades")
                        .HasForeignKey("DuracaoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GuardaCultura.Models.EstacaoAno", "EstacaoAno")
                        .WithMany("Atratividades")
                        .HasForeignKey("EstacaoAnoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GuardaCultura.Models.Miradouro", "Miradouro")
                        .WithMany("Atratividades")
                        .HasForeignKey("MiradouroId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GuardaCultura.Models.Fotografia", b =>
                {
                    b.HasOne("GuardaCultura.Models.EstacaoAno", "EstacaoAno")
                        .WithMany("Fotografias")
                        .HasForeignKey("EstacaoAnoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GuardaCultura.Models.Miradouro", "Miradouro")
                        .WithMany("Fotografias")
                        .HasForeignKey("MiradouroId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GuardaCultura.Models.Pessoa", "Pessoa")
                        .WithMany("Fotografias")
                        .HasForeignKey("PessoaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GuardaCultura.Models.TipoImagem", "TipoImagem")
                        .WithMany("Fotografias")
                        .HasForeignKey("TipoImagemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GuardaCultura.Models.Ocupacao", b =>
                {
                    b.HasOne("GuardaCultura.Models.Hora", "Hora")
                        .WithMany("Ocupacaos")
                        .HasForeignKey("HoraId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GuardaCultura.Models.Miradouro", "Miradouro")
                        .WithMany("Ocupacaos")
                        .HasForeignKey("MiradouroId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GuardaCultura.Models.Pessoa", b =>
                {
                    b.HasOne("GuardaCultura.Models.Funcao", "Funcao")
                        .WithMany("Pessoas")
                        .HasForeignKey("FuncaoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
