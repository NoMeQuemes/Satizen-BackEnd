﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Satizen_Api.Migrations
{
    /// <inheritdoc />
    public partial class SeAgreganDatos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Estados",
                keyColumn: "idEstado",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Estados",
                keyColumn: "idEstado",
                keyValue: 2);

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
        }
    }
}