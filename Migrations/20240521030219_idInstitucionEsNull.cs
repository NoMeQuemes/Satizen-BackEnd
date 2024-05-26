using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Satizen_Api.Migrations
{
    /// <inheritdoc />
    public partial class idInstitucionEsNull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pacientes_Instituciones_idInstitucion",
                table: "Pacientes");

            migrationBuilder.AlterColumn<int>(
                name: "idInstitucion",
                table: "Pacientes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Pacientes_Instituciones_idInstitucion",
                table: "Pacientes",
                column: "idInstitucion",
                principalTable: "Instituciones",
                principalColumn: "idUsuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pacientes_Instituciones_idInstitucion",
                table: "Pacientes");

            migrationBuilder.AlterColumn<int>(
                name: "idInstitucion",
                table: "Pacientes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Pacientes_Instituciones_idInstitucion",
                table: "Pacientes",
                column: "idInstitucion",
                principalTable: "Instituciones",
                principalColumn: "idUsuario",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
