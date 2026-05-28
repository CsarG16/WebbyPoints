using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebbyPoints.Migrations
{
    /// <inheritdoc />
    public partial class AddEmailUniqueIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 100,
                column: "FechaRegistro",
                value: new DateTime(2026, 5, 28, 13, 52, 27, 640, DateTimeKind.Local).AddTicks(314));

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 200,
                column: "FechaRegistro",
                value: new DateTime(2026, 5, 28, 13, 52, 27, 642, DateTimeKind.Local).AddTicks(3691));

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Email",
                table: "Usuarios",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Usuarios_Email",
                table: "Usuarios");

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 100,
                column: "FechaRegistro",
                value: new DateTime(2026, 4, 26, 23, 36, 36, 199, DateTimeKind.Local).AddTicks(3245));

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 200,
                column: "FechaRegistro",
                value: new DateTime(2026, 4, 26, 23, 36, 36, 201, DateTimeKind.Local).AddTicks(5838));
        }
    }
}
