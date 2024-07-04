﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using app.Data;

#nullable disable

namespace app.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240704160035_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.18")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("app.DTO.AlunoDTO", b =>
                {
                    b.Property<long?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long?>("Id"));

                    b.Property<string>("Cpf")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Endereco")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("LivroId")
                        .HasColumnType("bigint");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sexo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefone")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("LivroId");

                    b.ToTable("Alunos");
                });

            modelBuilder.Entity("app.DTO.EmprestimoDTO", b =>
                {
                    b.Property<long?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long?>("Id"));

                    b.Property<long?>("AlunoId")
                        .HasColumnType("bigint");

                    b.Property<long?>("AlunosId")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("Data")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("Hora")
                        .HasColumnType("datetime2");

                    b.Property<long?>("LivroId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("AlunosId");

                    b.ToTable("Emprestimos");
                });

            modelBuilder.Entity("app.DTO.LivroDTO", b =>
                {
                    b.Property<long?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long?>("Id"));

                    b.Property<string>("Autor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Genero")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Titulo")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Livros");
                });

            modelBuilder.Entity("app.DTO.UserDTO.UserDTO", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ErrorMessages")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("app.DTO.AlunoDTO", b =>
                {
                    b.HasOne("app.DTO.LivroDTO", "Livro")
                        .WithMany()
                        .HasForeignKey("LivroId");

                    b.Navigation("Livro");
                });

            modelBuilder.Entity("app.DTO.EmprestimoDTO", b =>
                {
                    b.HasOne("app.DTO.AlunoDTO", "Alunos")
                        .WithMany()
                        .HasForeignKey("AlunosId");

                    b.Navigation("Alunos");
                });
#pragma warning restore 612, 618
        }
    }
}
