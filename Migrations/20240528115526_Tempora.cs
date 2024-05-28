using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Satizen_Api.Migrations
{
    /// <inheritdoc />
    public partial class Tempora : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Roles_idRoles",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_idRoles",
                table: "Usuarios");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_idRoles",
                table: "Usuarios",
                column: "idRoles");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Roles_idRoles",
                table: "Usuarios",
                column: "idRoles",
                principalTable: "Roles",
                principalColumn: "idRol");
        }
    }
}
