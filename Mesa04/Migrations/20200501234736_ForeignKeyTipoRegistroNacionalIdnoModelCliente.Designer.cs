﻿// <auto-generated />
using System;
using Mesa04.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Mesa04.Migrations
{
    [DbContext(typeof(Mesa04Context))]
    [Migration("20200501234736_ForeignKeyTipoRegistroNacionalIdnoModelCliente")]
    partial class ForeignKeyTipoRegistroNacionalIdnoModelCliente
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Mesa04.Models.Cliente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("Aniversario")
                        .IsRequired();

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(60);

                    b.Property<int>("RegistroNacional");

                    b.Property<int>("TipoRegistroNacionalId");

                    b.HasKey("Id");

                    b.HasIndex("TipoRegistroNacionalId");

                    b.ToTable("Cliente");
                });

            modelBuilder.Entity("Mesa04.Models.Departamento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nome");

                    b.HasKey("Id");

                    b.ToTable("Departamento");
                });

            modelBuilder.Entity("Mesa04.Models.Operacao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Banco")
                        .IsRequired();

                    b.Property<int>("ClienteId");

                    b.Property<DateTime>("Data");

                    b.Property<double>("Despesa");

                    b.Property<int>("Fluxo");

                    b.Property<int>("OperacaoStatus");

                    b.Property<int>("OperadorId");

                    b.Property<double>("Taxa");

                    b.Property<int>("TipoOperacaoId");

                    b.Property<double>("Valor");

                    b.HasKey("Id");

                    b.HasIndex("ClienteId");

                    b.HasIndex("OperadorId");

                    b.ToTable("Operacao");
                });

            modelBuilder.Entity("Mesa04.Models.Operador", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Aniversario");

                    b.Property<int>("DepartamentoId");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(60);

                    b.Property<double>("SalarioBase");

                    b.HasKey("Id");

                    b.HasIndex("DepartamentoId");

                    b.ToTable("Operador");
                });

            modelBuilder.Entity("Mesa04.Models.TipoRegistroNacional", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nome");

                    b.HasKey("Id");

                    b.ToTable("TipoRegistroNacional");
                });

            modelBuilder.Entity("Mesa04.Models.Cliente", b =>
                {
                    b.HasOne("Mesa04.Models.TipoRegistroNacional", "TipoRegistroNacional")
                        .WithMany("Clientes")
                        .HasForeignKey("TipoRegistroNacionalId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Mesa04.Models.Operacao", b =>
                {
                    b.HasOne("Mesa04.Models.Cliente")
                        .WithMany("Operacoes")
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Mesa04.Models.Operador")
                        .WithMany("Operacoes")
                        .HasForeignKey("OperadorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Mesa04.Models.Operador", b =>
                {
                    b.HasOne("Mesa04.Models.Departamento", "Departamento")
                        .WithMany("Operadores")
                        .HasForeignKey("DepartamentoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
