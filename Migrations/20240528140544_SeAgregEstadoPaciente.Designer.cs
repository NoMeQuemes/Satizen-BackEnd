﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Satizen_Api.Data;

#nullable disable

namespace Satizen_Api.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240528140544_SeAgregEstadoPaciente")]
    partial class SeAgregEstadoPaciente
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Satizen_Api.Models.Institucion", b =>
                {
                    b.Property<int>("idInstitucion")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("idInstitucion"));

                    b.HasKey("idInstitucion");

                    b.ToTable("Instituciones");
                });

            modelBuilder.Entity("Satizen_Api.Models.Paciente", b =>
                {
                    b.Property<int>("idPaciente")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("idPaciente"));

                    b.Property<DateTime?>("estadoPaciente")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("fechaIngreso")
                        .HasColumnType("datetime2");

                    b.Property<int?>("idInstitucion")
                        .HasColumnType("int");

                    b.Property<int?>("idUsuario")
                        .HasColumnType("int");

                    b.Property<string>("nombrePaciente")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("numeroHabitacionPaciente")
                        .HasColumnType("int");

                    b.Property<string>("observacionPaciente")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("idPaciente");

                    b.HasIndex("idInstitucion");

                    b.HasIndex("idUsuario");

                    b.ToTable("Pacientes");
                });

            modelBuilder.Entity("Satizen_Api.Models.Permiso", b =>
                {
                    b.Property<int>("idPermiso")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("idPermiso"));

                    b.Property<string>("tipo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("idPermiso");

                    b.ToTable("Permisos");
                });

            modelBuilder.Entity("Satizen_Api.Models.Roles", b =>
                {
                    b.Property<int>("idRol")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("idRol"));

                    b.Property<string>("descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("idPermiso")
                        .HasColumnType("int");

                    b.Property<string>("nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("idRol");

                    b.HasIndex("idPermiso");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Satizen_Api.Models.Sectores", b =>
                {
                    b.Property<int>("idSector")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("idSector"));

                    b.Property<string>("descripcionSector")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("idInstitucion")
                        .HasColumnType("int");

                    b.Property<string>("nombreSector")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("idSector");

                    b.HasIndex("idInstitucion");

                    b.ToTable("Sectores");
                });

            modelBuilder.Entity("Satizen_Api.Models.Usuario", b =>
                {
                    b.Property<int>("idUsuario")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("idUsuario"));

                    b.Property<DateTime?>("estadoUsuario")
                        .HasColumnType("datetime2");

                    b.Property<int?>("idRoles")
                        .HasColumnType("int");

                    b.Property<string>("nombreUsuario")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("idUsuario");

                    b.HasIndex("idRoles");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("Satizen_Api.Models.Paciente", b =>
                {
                    b.HasOne("Satizen_Api.Models.Institucion", "institucion")
                        .WithMany()
                        .HasForeignKey("idInstitucion");

                    b.HasOne("Satizen_Api.Models.Usuario", "usuario")
                        .WithMany()
                        .HasForeignKey("idUsuario");

                    b.Navigation("institucion");

                    b.Navigation("usuario");
                });

            modelBuilder.Entity("Satizen_Api.Models.Roles", b =>
                {
                    b.HasOne("Satizen_Api.Models.Permiso", "Permiso")
                        .WithMany()
                        .HasForeignKey("idPermiso");

                    b.Navigation("Permiso");
                });

            modelBuilder.Entity("Satizen_Api.Models.Sectores", b =>
                {
                    b.HasOne("Satizen_Api.Models.Institucion", "Instituciones")
                        .WithMany()
                        .HasForeignKey("idInstitucion")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Instituciones");
                });

            modelBuilder.Entity("Satizen_Api.Models.Usuario", b =>
                {
                    b.HasOne("Satizen_Api.Models.Roles", "Roles")
                        .WithMany()
                        .HasForeignKey("idRoles");

                    b.Navigation("Roles");
                });
#pragma warning restore 612, 618
        }
    }
}
