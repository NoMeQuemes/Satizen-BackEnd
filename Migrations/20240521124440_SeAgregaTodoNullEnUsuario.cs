using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Satizen_Api.Migrations
{
    /// <inheritdoc />
    public partial class SeAgregaTodoNullEnUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Roles_Permisos_idPermiso",
                table: "Roles");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Estados_idEstado",
                table: "Usuarios");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Roles_idRoles",
                table: "Usuarios");

            migrationBuilder.AlterColumn<int>(
                name: "idRoles",
                table: "Usuarios",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "idEstado",
                table: "Usuarios",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "idPermiso",
                table: "Roles",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_Permisos_idPermiso",
                table: "Roles",
                column: "idPermiso",
                principalTable: "Permisos",
                principalColumn: "idPermiso");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Estados_idEstado",
                table: "Usuarios",
                column: "idEstado",
                principalTable: "Estados",
                principalColumn: "idEstado");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Roles_idRoles",
                table: "Usuarios",
                column: "idRoles",
                principalTable: "Roles",
                principalColumn: "idRol");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Roles_Permisos_idPermiso",
                table: "Roles");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Estados_idEstado",
                table: "Usuarios");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Roles_idRoles",
                table: "Usuarios");

            migrationBuilder.AlterColumn<int>(
                name: "idRoles",
                table: "Usuarios",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "idEstado",
                table: "Usuarios",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "idPermiso",
                table: "Roles",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_Permisos_idPermiso",
                table: "Roles",
                column: "idPermiso",
                principalTable: "Permisos",
                principalColumn: "idPermiso",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Estados_idEstado",
                table: "Usuarios",
                column: "idEstado",
                principalTable: "Estados",
                principalColumn: "idEstado",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Roles_idRoles",
                table: "Usuarios",
                column: "idRoles",
                principalTable: "Roles",
                principalColumn: "idRol",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
