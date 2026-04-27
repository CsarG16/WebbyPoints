using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebbyPoints.Migrations
{
    /// <inheritdoc />
    public partial class AddFaustoAdmin2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 100,
                column: "FechaRegistro",
                value: new DateTime(2026, 4, 26, 23, 36, 36, 199, DateTimeKind.Local).AddTicks(3245));

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "Carrera", "Edad", "Email", "EsAdmin", "FechaRegistro", "FotoUrl", "Nombre", "Password", "Preferencias", "Puntos", "Rango", "Universidad" },
                values: new object[] { 200, "Ingeniería de Computación y Sistemas", 21, "fausto_miranda@usmp.pe", true, new DateTime(2026, 4, 26, 23, 36, 36, 201, DateTimeKind.Local).AddTicks(5838), null, "Fausto Miranda", "Mimamamemima123", "Todo", 999, "Administrador", "USMP" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 200);

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 100,
                column: "FechaRegistro",
                value: new DateTime(2026, 4, 25, 1, 19, 32, 261, DateTimeKind.Local).AddTicks(7303));
        }
    }
}
