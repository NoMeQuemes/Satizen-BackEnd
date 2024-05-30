using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Satizen_Api.Migrations
{
    /// <inheritdoc />
    public partial class CreacionDeTablaAsignacion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Asignaciones_Personal_idPersonal",
                table: "Asignaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_Asignaciones_Sector_idSector",
                table: "Asignaciones");

            migrationBuilder.DropTable(
                name: "Personal");

            migrationBuilder.DropTable(
                name: "Sector");

            migrationBuilder.DropIndex(
                name: "IX_Asignaciones_idPersonal",
                table: "Asignaciones");

            migrationBuilder.DropIndex(
                name: "IX_Asignaciones_idSector",
                table: "Asignaciones");

            migrationBuilder.AddColumn<int>(
                name: "diaSemana",
                table: "Asignaciones",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "horaFinalizacion",
                table: "Asignaciones",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "horaInicio",
                table: "Asignaciones",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<string>(
                name: "turno",
                table: "Asignaciones",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "diaSemana",
                table: "Asignaciones");

            migrationBuilder.DropColumn(
                name: "horaFinalizacion",
                table: "Asignaciones");

            migrationBuilder.DropColumn(
                name: "horaInicio",
                table: "Asignaciones");

            migrationBuilder.DropColumn(
                name: "turno",
                table: "Asignaciones");

            migrationBuilder.CreateTable(
                name: "Personal",
                columns: table => new
                {
                    idPersonal = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    celularPersonal = table.Column<int>(type: "int", nullable: false),
                    correoPersonal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    idInstitucion = table.Column<int>(type: "int", nullable: false),
                    nombrePersonal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    rolPersonal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    telefonoPersonal = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personal", x => x.idPersonal);
                });

            migrationBuilder.CreateTable(
                name: "Sector",
                columns: table => new
                {
                    idSector = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idInstitucion = table.Column<int>(type: "int", nullable: false),
                    nombreSector = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sector", x => x.idSector);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Asignaciones_idPersonal",
                table: "Asignaciones",
                column: "idPersonal");

            migrationBuilder.CreateIndex(
                name: "IX_Asignaciones_idSector",
                table: "Asignaciones",
                column: "idSector");

            migrationBuilder.AddForeignKey(
                name: "FK_Asignaciones_Personal_idPersonal",
                table: "Asignaciones",
                column: "idPersonal",
                principalTable: "Personal",
                principalColumn: "idPersonal",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Asignaciones_Sector_idSector",
                table: "Asignaciones",
                column: "idSector",
                principalTable: "Sector",
                principalColumn: "idSector",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
