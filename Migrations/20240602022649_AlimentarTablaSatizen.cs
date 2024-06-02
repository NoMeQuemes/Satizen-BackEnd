using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Proyec_Satizen_Api.Migrations
{
    /// <inheritdoc />
    public partial class AlimentarTablaSatizen : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Institucions",
                columns: new[] { "idInstitucion", "celularInstitucion", "correoInstitucion", "direccionInstitucion", "nombreInstitucion", "telefonoInstitucion" },
                values: new object[] { 1, "6473467326", "santaqgmail.com", "Caleee", "Santa", "53625362" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Institucions",
                keyColumn: "idInstitucion",
                keyValue: 1);
        }
    }
}
