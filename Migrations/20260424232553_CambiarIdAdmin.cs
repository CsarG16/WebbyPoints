using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebbyPoints.Migrations
{
    /// <inheritdoc />
    public partial class CambiarIdAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 99);

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "Carrera", "Edad", "Email", "EsAdmin", "FechaRegistro", "FotoUrl", "Nombre", "Password", "Preferencias", "Puntos", "Rango", "Universidad" },
                values: new object[] { 100, "Ingeniería de Computación y Sistemas", 21, "cesar_paredes7@usmp.pe", true, new DateTime(2026, 4, 24, 18, 25, 53, 254, DateTimeKind.Local).AddTicks(8849), null, "César Paredes", "Vinicola14//", "Todo", 999, "Administrador", "USMP" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 100);

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "Carrera", "Edad", "Email", "EsAdmin", "FechaRegistro", "FotoUrl", "Nombre", "Password", "Preferencias", "Puntos", "Rango", "Universidad" },
                values: new object[] { 99, "Sistemas", 20, "cesar_paredes7@usmp.pe", true, new DateTime(2026, 4, 24, 18, 20, 5, 819, DateTimeKind.Local).AddTicks(8411), null, "César Paredes", "Vinicola14//", "Todo", 999, "Administrador", "USMP" });
        }
    }
}
