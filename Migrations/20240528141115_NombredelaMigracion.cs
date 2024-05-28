using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Satizen_Api.Migrations
{
    /// <inheritdoc />
    public partial class NombredelaMigracion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Personal",
                columns: table => new
                {
                    idPersonal = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idInstitucion = table.Column<int>(type: "int", nullable: false),
                    nombrePersonal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    rolPersonal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    celularPersonal = table.Column<int>(type: "int", nullable: false),
                    telefonoPersonal = table.Column<int>(type: "int", nullable: false),
                    correoPersonal = table.Column<string>(type: "nvarchar(max)", nullable: true)
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

            migrationBuilder.CreateTable(
                name: "Asignaciones",
                columns: table => new
                {
                    idAsignacion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idPersonal = table.Column<int>(type: "int", nullable: false),
                    idSector = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Asignaciones", x => x.idAsignacion);
                    table.ForeignKey(
                        name: "FK_Asignaciones_Personal_idPersonal",
                        column: x => x.idPersonal,
                        principalTable: "Personal",
                        principalColumn: "idPersonal",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Asignaciones_Sector_idSector",
                        column: x => x.idSector,
                        principalTable: "Sector",
                        principalColumn: "idSector",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Asignaciones_idPersonal",
                table: "Asignaciones",
                column: "idPersonal");

            migrationBuilder.CreateIndex(
                name: "IX_Asignaciones_idSector",
                table: "Asignaciones",
                column: "idSector");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Asignaciones");

            migrationBuilder.DropTable(
                name: "Personal");

            migrationBuilder.DropTable(
                name: "Sector");
        }
    }
}
