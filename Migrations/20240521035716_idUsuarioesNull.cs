using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Satizen_Api.Migrations
{
    /// <inheritdoc />
    public partial class idUsuarioesNull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pacientes_Usuarios_idUsuario",
                table: "Pacientes");

            migrationBuilder.RenameColumn(
                name: "idUsuario",
                table: "Instituciones",
                newName: "idInstitucion");

            migrationBuilder.AlterColumn<int>(
                name: "idUsuario",
                table: "Pacientes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Pacientes_Usuarios_idUsuario",
                table: "Pacientes",
                column: "idUsuario",
                principalTable: "Usuarios",
                principalColumn: "idUsuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pacientes_Usuarios_idUsuario",
                table: "Pacientes");

            migrationBuilder.RenameColumn(
                name: "idInstitucion",
                table: "Instituciones",
                newName: "idUsuario");

            migrationBuilder.AlterColumn<int>(
                name: "idUsuario",
                table: "Pacientes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Pacientes_Usuarios_idUsuario",
                table: "Pacientes",
                column: "idUsuario",
                principalTable: "Usuarios",
                principalColumn: "idUsuario",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
