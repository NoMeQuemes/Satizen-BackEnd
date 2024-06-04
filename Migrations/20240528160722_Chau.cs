using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Satizen_Api.Migrations
{
    /// <inheritdoc />
    public partial class Chau : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pacientes_Instituciones_idInstitucion",
                table: "Pacientes");

            migrationBuilder.DropForeignKey(
                name: "FK_Sectores_Instituciones_idInstitucion",
                table: "Sectores");

            migrationBuilder.DropTable(
                name: "Instituciones");

            migrationBuilder.DropIndex(
                name: "IX_Sectores_idInstitucion",
                table: "Sectores");

            migrationBuilder.DropIndex(
                name: "IX_Pacientes_idInstitucion",
                table: "Pacientes");

            migrationBuilder.DropColumn(
                name: "idInstitucion",
                table: "Sectores");

            migrationBuilder.DropColumn(
                name: "idInstitucion",
                table: "Pacientes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "idInstitucion",
                table: "Sectores",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "idInstitucion",
                table: "Pacientes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Instituciones",
                columns: table => new
                {
                    idInstitucion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instituciones", x => x.idInstitucion);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sectores_idInstitucion",
                table: "Sectores",
                column: "idInstitucion");

            migrationBuilder.CreateIndex(
                name: "IX_Pacientes_idInstitucion",
                table: "Pacientes",
                column: "idInstitucion");

            migrationBuilder.AddForeignKey(
                name: "FK_Pacientes_Instituciones_idInstitucion",
                table: "Pacientes",
                column: "idInstitucion",
                principalTable: "Instituciones",
                principalColumn: "idInstitucion");

            migrationBuilder.AddForeignKey(
                name: "FK_Sectores_Instituciones_idInstitucion",
                table: "Sectores",
                column: "idInstitucion",
                principalTable: "Instituciones",
                principalColumn: "idInstitucion",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
