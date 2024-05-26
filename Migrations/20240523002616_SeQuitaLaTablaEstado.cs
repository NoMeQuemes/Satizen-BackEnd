using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Satizen_Api.Migrations
{
    /// <inheritdoc />
    public partial class SeQuitaLaTablaEstado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Estados_idEstado",
                table: "Usuarios");

            migrationBuilder.DropTable(
                name: "Estados");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_idEstado",
                table: "Usuarios");

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "idPermiso",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "idRol",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "idRol",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "idPermiso",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "idPermiso",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "idEstado",
                table: "Usuarios");

            migrationBuilder.AddColumn<DateTime>(
                name: "estadoUsuario",
                table: "Usuarios",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "estadoUsuario",
                table: "Usuarios");

            migrationBuilder.AddColumn<int>(
                name: "idEstado",
                table: "Usuarios",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Estados",
                columns: table => new
                {
                    idEstado = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tipo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estados", x => x.idEstado);
                });

            migrationBuilder.InsertData(
                table: "Estados",
                columns: new[] { "idEstado", "tipo" },
                values: new object[,]
                {
                    { 1, "Activo" },
                    { 2, "Inactivo" }
                });

            migrationBuilder.InsertData(
                table: "Permisos",
                columns: new[] { "idPermiso", "tipo" },
                values: new object[,]
                {
                    { 1, "Crear" },
                    { 2, "Leer" },
                    { 3, "Eliminar" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "idRol", "descripcion", "idPermiso", "nombre" },
                values: new object[,]
                {
                    { 1, "Soy administrador", 1, "Administrador" },
                    { 2, "Soy médico", 2, "medico" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_idEstado",
                table: "Usuarios",
                column: "idEstado");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Estados_idEstado",
                table: "Usuarios",
                column: "idEstado",
                principalTable: "Estados",
                principalColumn: "idEstado");
        }
    }
}
