﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Satizen_Api.Data;

#nullable disable

namespace Satizen_Api.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

                b.Property<string>("celularInstitucion")
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("correoInstitucion")
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("direccionInstitucion")
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("nombreInstitucion")
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("telefonoInstitucion")
                    .HasColumnType("nvarchar(max)");

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

                b.HasData(
                    new
                    {
                        idPermiso = 1,
                        tipo = "Crear"
                    },
                    new
                    {
                        idPermiso = 2,
                        tipo = "Leer"
                    },
                    new
                    {
                        idPermiso = 3,
                        tipo = "Eliminar"
                    },
                    new
                    {
                        idPermiso = 4,
                        tipo = "Actualizar"
                    });
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

                b.HasData(
                    new
                    {
                        idRol = 1,
                        descripcion = "Soy administrador",
                        idPermiso = 1,
                        nombre = "Administrador"
                    },
                    new
                    {
                        idRol = 2,
                        descripcion = "Soy médico",
                        idPermiso = 2,
                        nombre = "Medico"
                    },
                    new
                    {
                        idRol = 3,
                        descripcion = "Soy enfermero",
                        idPermiso = 2,
                        nombre = "Enfermero"
                    });
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

            modelBuilder.Entity("Satizen_Api.Models.Asignacion", b =>
            {
                b.Property<int>("idAsignacion")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("idAsignacion"));

                b.Property<DateTime>("diaSemana")
                    .HasColumnType("datetime2");

                b.Property<TimeSpan>("horaFinalizacion")
                    .HasColumnType("time");

                b.Property<TimeSpan>("horaInicio")
                    .HasColumnType("time");

                b.Property<int>("idPersonal")
                    .HasColumnType("int");

                b.Property<int>("idSector")
                    .HasColumnType("int");

                b.Property<string>("turno")
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnType("nvarchar(50)");

                b.HasKey("idAsignacion");

                b.HasIndex("idPersonal");

                b.HasIndex("idSector");

                b.ToTable("Asignaciones");
            });

            modelBuilder.Entity("Satizen_Api.Models.DispositivoLaboral", b =>
            {
                b.Property<int>("idCelular")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("idCelular"));

                b.Property<int>("idPersonal")
                    .HasColumnType("int");

                b.Property<string>("numCelular")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("observacionCelular")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.HasKey("idCelular");

                b.HasIndex("idPersonal");

                b.ToTable("DispositivosLaborales");
            });

            modelBuilder.Entity("Satizen_Api.Models.Personal", b =>
            {
                b.Property<int>("idPersonal")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("idPersonal"));

                b.Property<int>("celularPersonal")
                    .HasColumnType("int");

                b.Property<string>("correoPersonal")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<int?>("idInstitucion")
                    .HasColumnType("int");

                b.Property<string>("nombrePersonal")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("rolPersonal")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<int>("telefonoPersonal")
                    .HasColumnType("int");

                b.HasKey("idPersonal");

                b.HasIndex("idInstitucion");

                b.ToTable("Personals");
            });

            modelBuilder.Entity("Satizen_Api.Models.RefreshToken", b =>
            {
                b.Property<int>("idRefreshToken")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("idRefreshToken"));

                b.Property<bool>("esActivo")
                    .ValueGeneratedOnAddOrUpdate()
                    .HasColumnType("bit")
                    .HasComputedColumnSql("IIF(fechaExpiracion < GETDATE(), CONVERT(BIT, 0), CONVERT(BIT, 1))");

                b.Property<DateTime>("fechaCreacion")
                    .HasColumnType("datetime2");

                b.Property<DateTime>("fechaExpiracion")
                    .HasColumnType("datetime2");

                b.Property<int>("idUsuario")
                    .HasColumnType("int");

                b.Property<string>("refreshToken")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("token")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.HasKey("idRefreshToken");

                b.HasIndex("idUsuario");

                b.ToTable("RefreshTokens");
            });

            modelBuilder.Entity("Satizen_Api.Models.Sector", b =>
            {
                b.Property<int>("idSector")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("idSector"));

                b.Property<int>("idInstitucion")
                    .HasColumnType("int");

                b.Property<string>("nombreSector")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("observacionSector")
                    .HasColumnType("nvarchar(max)");

                b.HasKey("idSector");

                b.HasIndex("idInstitucion");

                b.ToTable("Sectores");
            });

            modelBuilder.Entity("Satizen_Api.Models.Paciente", b =>
            {
                b.HasOne("Satizen_Api.Models.Institucion", "Institucion")
                    .WithMany()
                    .HasForeignKey("idInstitucion");

                b.HasOne("Satizen_Api.Models.Usuario", "Usuario")
                    .WithMany()
                    .HasForeignKey("idUsuario");

                b.Navigation("Institucion");

                b.Navigation("Usuario");
            });

            modelBuilder.Entity("Satizen_Api.Models.Roles", b =>
            {
                b.HasOne("Satizen_Api.Models.Permiso", "Permiso")
                    .WithMany()
                    .HasForeignKey("idPermiso");

                b.Navigation("Permiso");
            });

            modelBuilder.Entity("Satizen_Api.Models.Usuario", b =>
            {
                b.HasOne("Satizen_Api.Models.Roles", "Roles")
                    .WithMany()
                    .HasForeignKey("idRoles");

                b.Navigation("Roles");
            });

            modelBuilder.Entity("Satizen_Api.Models.Asignacion", b =>
            {
                b.HasOne("Satizen_Api.Models.Personal", "Personal")
                    .WithMany()
                    .HasForeignKey("idPersonal")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.HasOne("Satizen_Api.Models.Sector", "Sector")
                    .WithMany()
                    .HasForeignKey("idSector")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.Navigation("Personal");

                b.Navigation("Sector");
            });

            modelBuilder.Entity("Satizen_Api.Models.DispositivoLaboral", b =>
            {
                b.HasOne("Satizen_Api.Models.Personal", "Personal")
                    .WithMany()
                    .HasForeignKey("idPersonal")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.Navigation("Personal");
            });

            modelBuilder.Entity("Satizen_Api.Models.Personal", b =>
            {
                b.HasOne("Satizen_Api.Models.Institucion", "Institucion")
                    .WithMany()
                    .HasForeignKey("idInstitucion");

                b.Navigation("Institucion");
            });

            modelBuilder.Entity("Satizen_Api.Models.RefreshToken", b =>
            {
                b.HasOne("Satizen_Api.Models.Usuario", "Usuario")
                    .WithMany("RefreshTokens")
                    .HasForeignKey("idUsuario")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.Navigation("Usuario");
            });

            modelBuilder.Entity("Satizen_Api.Models.Sector", b =>
            {
                b.HasOne("Satizen_Api.Models.Institucion", "Institucion")
                    .WithMany()
                    .HasForeignKey("idInstitucion")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.Navigation("Institucion");
            });

            modelBuilder.Entity("Satizen_Api.Models.Usuario", b =>
            {
                b.Navigation("RefreshTokens");
            });
#pragma warning restore 612, 618
        }
    }
}
