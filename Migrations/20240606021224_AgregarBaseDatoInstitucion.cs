using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Satizen_Api.Migrations
{
    /// <inheritdoc />
    public partial class AgregarBaseDatoInstitucion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "celularInstitucion",
                table: "Instituciones",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "correoInstitucion",
                table: "Instituciones",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "direccionInstitucion",
                table: "Instituciones",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "nombreInstitucion",
                table: "Instituciones",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "telefonoInstitucion",
                table: "Instituciones",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Instituciones",
                columns: new[] { "idInstitucion", "celularInstitucion", "correoInstitucion", "direccionInstitucion", "nombreInstitucion", "telefonoInstitucion" },
                values: new object[] { 1, "6473467326", "santaqgmail.com", "Caleee", "Santa", "53625362" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Instituciones",
                keyColumn: "idInstitucion",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "celularInstitucion",
                table: "Instituciones");

            migrationBuilder.DropColumn(
                name: "correoInstitucion",
                table: "Instituciones");

            migrationBuilder.DropColumn(
                name: "direccionInstitucion",
                table: "Instituciones");

            migrationBuilder.DropColumn(
                name: "nombreInstitucion",
                table: "Instituciones");

            migrationBuilder.DropColumn(
                name: "telefonoInstitucion",
                table: "Instituciones");
        }
    }
}
