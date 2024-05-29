using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Satizen_Api.Migrations
{
    /// <inheritdoc />
    public partial class Llamados : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Llamados",
                columns: table => new
                {
                    idLlamado = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idPaciente = table.Column<int>(type: "int", nullable: false),
                    idPersonal = table.Column<int>(type: "int", nullable: false),
                    fechaHoraLlamado = table.Column<DateTime>(type: "datetime2", nullable: false),
                    estadoLlamado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    prioridadLlamado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    observacionLlamado = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Llamados", x => x.idLlamado);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Llamados");
        }
    }
}
