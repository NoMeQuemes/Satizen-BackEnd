using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Satizen_Api.Migrations
{
    /// <inheritdoc />
    public partial class SeAgregaSectores : Migration
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
                name: "Sectores",
                columns: table => new
                {
                    idSector = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idInstitucion = table.Column<int>(type: "int", nullable: false),
                    nombreSector = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    descripcionSector = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sectores", x => x.idSector);
                    table.ForeignKey(
                        name: "FK_Sectores_Instituciones_idInstitucion",
                        column: x => x.idInstitucion,
                        principalTable: "Instituciones",
                        principalColumn: "idInstitucion",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sectores_idInstitucion",
                table: "Sectores",
                column: "idInstitucion");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sectores");

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
