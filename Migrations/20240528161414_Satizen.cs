using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Satizen_Api.Migrations
{
    /// <inheritdoc />
    public partial class Satizen : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Instituciones",
                columns: table => new
                {
                    idInstitucion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instituciones", x => x.idInstitucion);
                });

            migrationBuilder.CreateTable(
                name: "Pacientes",
                columns: table => new
                {
                    idPaciente = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idUsuario = table.Column<int>(type: "int", nullable: false),
                    idInstitucion = table.Column<int>(type: "int", nullable: false),
                    nombrePaciente = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    numeroHabitacionPaciente = table.Column<int>(type: "int", nullable: false),
                    fechaIngreso = table.Column<DateTime>(type: "datetime2", nullable: false),
                    estadoPaciente = table.Column<DateTime>(type: "datetime2", nullable: true),
                    observacionPaciente = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pacientes", x => x.idPaciente);
                });

            migrationBuilder.CreateTable(
                name: "Permisos",
                columns: table => new
                {
                    idPermiso = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tipo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permisos", x => x.idPermiso);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    idRol = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    idPermiso = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.idRol);
                    table.ForeignKey(
                        name: "FK_Roles_Permisos_idPermiso",
                        column: x => x.idPermiso,
                        principalTable: "Permisos",
                        principalColumn: "idPermiso");
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    idUsuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombreUsuario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    idRoles = table.Column<int>(type: "int", nullable: true),
                    estadoUsuario = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.idUsuario);
                    table.ForeignKey(
                        name: "FK_Usuarios_Roles_idRoles",
                        column: x => x.idRoles,
                        principalTable: "Roles",
                        principalColumn: "idRol");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Roles_idPermiso",
                table: "Roles",
                column: "idPermiso");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_idRoles",
                table: "Usuarios",
                column: "idRoles");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Instituciones");

            migrationBuilder.DropTable(
                name: "Pacientes");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Permisos");
        }
    }
}
