﻿// <auto-generated />
using System;
using BackEnd.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BackEnd.Migrations
{
    [DbContext(typeof(AmbulantaContext))]
    [Migration("20210822162424_V2")]
    partial class V2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BackEnd.Models.Ambulanta", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Adresa")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("Adresa");

                    b.Property<int>("BrojPunktova")
                        .HasColumnType("int");

                    b.Property<string>("Grad")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("Grad");

                    b.Property<string>("Ime")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("Ime");

                    b.HasKey("ID");

                    b.ToTable("Ambulanta");
                });

            modelBuilder.Entity("BackEnd.Models.Gradjanin", b =>
                {
                    b.Property<string>("JMBG")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("JMBG");

                    b.Property<int?>("AmbulantaID")
                        .HasColumnType("int");

                    b.Property<string>("BrojTelefona")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)")
                        .HasColumnName("BrojTelefona");

                    b.Property<string>("Ime")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("Ime");

                    b.Property<string>("Prezime")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("Prezime");

                    b.Property<int?>("VakcinaID")
                        .HasColumnType("int");

                    b.HasKey("JMBG");

                    b.HasIndex("AmbulantaID");

                    b.HasIndex("VakcinaID");

                    b.ToTable("Gradjanin");
                });

            modelBuilder.Entity("BackEnd.Models.Vakcina", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AmbulantaID")
                        .HasColumnType("int");

                    b.Property<int>("BrojVakcinisanih")
                        .HasColumnType("int")
                        .HasColumnName("BrojVakcinisanih");

                    b.Property<string>("ImeVakcine")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("ImeVakcine");

                    b.Property<int>("Kolicina")
                        .HasColumnType("int")
                        .HasColumnName("Kolicina");

                    b.HasKey("ID");

                    b.HasIndex("AmbulantaID");

                    b.ToTable("Vakcina");
                });

            modelBuilder.Entity("BackEnd.Models.Gradjanin", b =>
                {
                    b.HasOne("BackEnd.Models.Ambulanta", null)
                        .WithMany("Gradjani")
                        .HasForeignKey("AmbulantaID");

                    b.HasOne("BackEnd.Models.Vakcina", "Vakcina")
                        .WithMany()
                        .HasForeignKey("VakcinaID");

                    b.Navigation("Vakcina");
                });

            modelBuilder.Entity("BackEnd.Models.Vakcina", b =>
                {
                    b.HasOne("BackEnd.Models.Ambulanta", null)
                        .WithMany("Vakcine")
                        .HasForeignKey("AmbulantaID");
                });

            modelBuilder.Entity("BackEnd.Models.Ambulanta", b =>
                {
                    b.Navigation("Gradjani");

                    b.Navigation("Vakcine");
                });
#pragma warning restore 612, 618
        }
    }
}
