using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Satizen_Api.Migrations
{
    /// <inheritdoc />
    public partial class SeAgregEstadoPaciente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "estadoPaciente",
                table: "Pacientes",
                type: "datetime2",
                nullable: true);

            // No elimines datos en la migración para evitar conflictos referenciales.
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "estadoPaciente",
                table: "Pacientes");

            // Si necesitas restaurar los datos en el Down, puedes insertarlos aquí,
            // pero asegúrate de que esto no cause problemas de referencia.
        }
    }
}
