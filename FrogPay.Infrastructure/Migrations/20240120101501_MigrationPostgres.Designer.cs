﻿// <auto-generated />
using System;
using FrogPay.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FrogPay.Infrastructure.Migrations
{
    [DbContext(typeof(FrogPayContext))]
    [Migration("20240120101501_MigrationPostgres")]
    partial class MigrationPostgres
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("FrogPay.Domain.Entities.DadosBancarios", b =>
                {
                    b.Property<Guid>("IdPessoa")
                        .HasColumnType("uuid");

                    b.Property<string>("Agencia")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CodigoBanco")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("DigitoConta")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("IdPessoa");

                    b.ToTable("DadosBancarios");
                });

            modelBuilder.Entity("FrogPay.Domain.Entities.Endereco", b =>
                {
                    b.Property<Guid>("IdPessoa")
                        .HasColumnType("uuid");

                    b.Property<string>("Bairro")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Cidade")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Complemento")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Logradouro")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Numero")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UFEstado")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("IdPessoa");

                    b.ToTable("Enderecos");
                });

            modelBuilder.Entity("FrogPay.Domain.Entities.Loja", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("CNPJ")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("DataAbertura")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("IdPessoa")
                        .HasColumnType("uuid");

                    b.Property<string>("NomeFantasia")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("RazaoSocial")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("IdPessoa")
                        .IsUnique();

                    b.ToTable("Lojas");
                });

            modelBuilder.Entity("FrogPay.Domain.Entities.Pessoa", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("Ativo")
                        .HasColumnType("boolean");

                    b.Property<string>("CPF")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Pessoas");
                });

            modelBuilder.Entity("FrogPay.Domain.Entities.DadosBancarios", b =>
                {
                    b.HasOne("FrogPay.Domain.Entities.Pessoa", "Pessoa")
                        .WithOne("DadosBancarios")
                        .HasForeignKey("FrogPay.Domain.Entities.DadosBancarios", "IdPessoa")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pessoa");
                });

            modelBuilder.Entity("FrogPay.Domain.Entities.Endereco", b =>
                {
                    b.HasOne("FrogPay.Domain.Entities.Pessoa", "Pessoa")
                        .WithOne("Endereco")
                        .HasForeignKey("FrogPay.Domain.Entities.Endereco", "IdPessoa")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pessoa");
                });

            modelBuilder.Entity("FrogPay.Domain.Entities.Loja", b =>
                {
                    b.HasOne("FrogPay.Domain.Entities.Pessoa", "Pessoa")
                        .WithOne("Loja")
                        .HasForeignKey("FrogPay.Domain.Entities.Loja", "IdPessoa")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pessoa");
                });

            modelBuilder.Entity("FrogPay.Domain.Entities.Pessoa", b =>
                {
                    b.Navigation("DadosBancarios")
                        .IsRequired();

                    b.Navigation("Endereco")
                        .IsRequired();

                    b.Navigation("Loja")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
