using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebbyPoints.Migrations
{
    /// <inheritdoc />
    public partial class ActualizarImagenBodega : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "PuntosInteres",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImagenUrl",
                value: "/images/bodega_esquina.png");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 100,
                column: "FechaRegistro",
                value: new DateTime(2026, 4, 25, 1, 19, 32, 261, DateTimeKind.Local).AddTicks(7303));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "PuntosInteres",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImagenUrl",
                value: "https://images.unsplash.com/photo-1514933651103-005eec06c04b");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 100,
                column: "FechaRegistro",
                value: new DateTime(2026, 4, 24, 18, 25, 53, 254, DateTimeKind.Local).AddTicks(8849));
        }
    }
}
