using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Satizen_Api.Migrations
{
    /// <inheritdoc />
    public partial class agus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contactos",
                columns: table => new
                {
                    idContacto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
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
        }
    }
}
