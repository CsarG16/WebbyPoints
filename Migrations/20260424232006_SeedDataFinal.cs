using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebbyPoints.Migrations
{
    /// <inheritdoc />
    public partial class SeedDataFinal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "EsAdmin",
                table: "Usuarios",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "Carrera", "Edad", "Email", "EsAdmin", "FechaRegistro", "FotoUrl", "Nombre", "Password", "Preferencias", "Puntos", "Rango", "Universidad" },
                values: new object[] { 99, "Sistemas", 20, "cesar_paredes7@usmp.pe", true, new DateTime(2026, 4, 24, 18, 20, 5, 819, DateTimeKind.Local).AddTicks(8411), null, "César Paredes", "Vinicola14//", "Todo", 999, "Administrador", "USMP" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 99);

            migrationBuilder.DropColumn(
                name: "EsAdmin",
                table: "Usuarios");
        }
    }
}
