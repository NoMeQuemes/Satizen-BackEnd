using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Satizen_Api.Migrations
{
    /// <inheritdoc />
    public partial class tbTurno : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "turno",
                table: "Asignaciones");

            migrationBuilder.AddColumn<int>(
                name: "TurnoId",
                table: "Asignaciones",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Turnos",
                columns: table => new
                {
                    TurnoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turnos", x => x.TurnoId);
                });

            migrationBuilder.InsertData(
                table: "Turnos",
                columns: new[] { "TurnoId", "Nombre" },
                values: new object[,]
                {
                    { 1, "Mañana" },
                    { 2, "Tarde" },
                    { 3, "Noche" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Asignaciones_TurnoId",
                table: "Asignaciones",
                column: "TurnoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Asignaciones_Turnos_TurnoId",
                table: "Asignaciones",
                column: "TurnoId",
                principalTable: "Turnos",
                principalColumn: "TurnoId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Asignaciones_Turnos_TurnoId",
                table: "Asignaciones");

            migrationBuilder.DropTable(
                name: "Turnos");

            migrationBuilder.DropIndex(
                name: "IX_Asignaciones_TurnoId",
                table: "Asignaciones");

            migrationBuilder.DropColumn(
                name: "TurnoId",
                table: "Asignaciones");

            migrationBuilder.AddColumn<string>(
                name: "turno",
                table: "Asignaciones",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
