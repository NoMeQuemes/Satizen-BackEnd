using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Satizen_Api.Migrations
{
    /// <inheritdoc />
    public partial class Contacto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "idPermiso",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "idPermiso",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "idRol",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "idRol",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "idRol",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "idPermiso",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "idPermiso",
                keyValue: 2);

            migrationBuilder.CreateTable(
                name: "Contactos",
                columns: table => new
                {
                    idContacto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idPaciente = table.Column<int>(type: "int", nullable: false),
                    celularPaciente = table.Column<int>(type: "int", nullable: false),
                    celularAcompananteP = table.Column<int>(type: "int", nullable: false),
                    FechaInicioValidez = table.Column<DateTime>(type: "datetime2", nullable: false),
                    estadoContacto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    eliminado = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contactos", x => x.idContacto);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contactos");

            migrationBuilder.InsertData(
                table: "Permisos",
                columns: new[] { "idPermiso", "tipo" },
                values: new object[,]
                {
                    { 1, "Crear" },
                    { 2, "Leer" },
                    { 3, "Eliminar" },
                    { 4, "Actualizar" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "idRol", "descripcion", "idPermiso", "nombre" },
                values: new object[,]
                {
                    { 1, "Soy administrador", 1, "Administrador" },
                    { 2, "Soy médico", 2, "Medico" },
                    { 3, "Soy enfermero", 2, "Enfermero" }
                });
        }
    }
}
