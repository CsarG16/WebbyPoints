using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebbyPoints.Migrations
{
    /// <inheritdoc />
    public partial class AddCheckInTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CheckIns",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UsuarioId = table.Column<int>(type: "INTEGER", nullable: false),
                    PuntoInteresId = table.Column<int>(type: "INTEGER", nullable: false),
                    FechaCheckIn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CodigoAsistencia = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    PuntosOtorgados = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckIns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CheckIns_PuntosInteres_PuntoInteresId",
                        column: x => x.PuntoInteresId,
                        principalTable: "PuntosInteres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CheckIns_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Recompensas",
                keyColumn: "Id",
                keyValue: 1,
                column: "FechaCreacion",
                value: new DateTime(2026, 5, 28, 22, 45, 1, 49, DateTimeKind.Local).AddTicks(2870));

            migrationBuilder.UpdateData(
                table: "Recompensas",
                keyColumn: "Id",
                keyValue: 2,
                column: "FechaCreacion",
                value: new DateTime(2026, 5, 28, 22, 45, 1, 49, DateTimeKind.Local).AddTicks(4215));

            migrationBuilder.UpdateData(
                table: "Recompensas",
                keyColumn: "Id",
                keyValue: 3,
                column: "FechaCreacion",
                value: new DateTime(2026, 5, 28, 22, 45, 1, 49, DateTimeKind.Local).AddTicks(4218));

            migrationBuilder.UpdateData(
                table: "Recompensas",
                keyColumn: "Id",
                keyValue: 4,
                column: "FechaCreacion",
                value: new DateTime(2026, 5, 28, 22, 45, 1, 49, DateTimeKind.Local).AddTicks(4220));

            migrationBuilder.UpdateData(
                table: "Recompensas",
                keyColumn: "Id",
                keyValue: 5,
                column: "FechaCreacion",
                value: new DateTime(2026, 5, 28, 22, 45, 1, 49, DateTimeKind.Local).AddTicks(4221));

            migrationBuilder.UpdateData(
                table: "Recompensas",
                keyColumn: "Id",
                keyValue: 6,
                column: "FechaCreacion",
                value: new DateTime(2026, 5, 28, 22, 45, 1, 49, DateTimeKind.Local).AddTicks(4222));

            migrationBuilder.UpdateData(
                table: "Recompensas",
                keyColumn: "Id",
                keyValue: 7,
                column: "FechaCreacion",
                value: new DateTime(2026, 5, 28, 22, 45, 1, 49, DateTimeKind.Local).AddTicks(4223));

            migrationBuilder.UpdateData(
                table: "Recompensas",
                keyColumn: "Id",
                keyValue: 8,
                column: "FechaCreacion",
                value: new DateTime(2026, 5, 28, 22, 45, 1, 49, DateTimeKind.Local).AddTicks(4224));

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 100,
                column: "FechaRegistro",
                value: new DateTime(2026, 5, 28, 22, 45, 1, 46, DateTimeKind.Local).AddTicks(6606));

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 200,
                column: "FechaRegistro",
                value: new DateTime(2026, 5, 28, 22, 45, 1, 48, DateTimeKind.Local).AddTicks(4491));

            migrationBuilder.CreateIndex(
                name: "IX_CheckIns_PuntoInteresId",
                table: "CheckIns",
                column: "PuntoInteresId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckIns_UsuarioId",
                table: "CheckIns",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CheckIns");

            migrationBuilder.UpdateData(
                table: "Recompensas",
                keyColumn: "Id",
                keyValue: 1,
                column: "FechaCreacion",
                value: new DateTime(2026, 5, 28, 22, 6, 29, 955, DateTimeKind.Local).AddTicks(172));

            migrationBuilder.UpdateData(
                table: "Recompensas",
                keyColumn: "Id",
                keyValue: 2,
                column: "FechaCreacion",
                value: new DateTime(2026, 5, 28, 22, 6, 29, 955, DateTimeKind.Local).AddTicks(2718));

            migrationBuilder.UpdateData(
                table: "Recompensas",
                keyColumn: "Id",
                keyValue: 3,
                column: "FechaCreacion",
                value: new DateTime(2026, 5, 28, 22, 6, 29, 955, DateTimeKind.Local).AddTicks(2725));

            migrationBuilder.UpdateData(
                table: "Recompensas",
                keyColumn: "Id",
                keyValue: 4,
                column: "FechaCreacion",
                value: new DateTime(2026, 5, 28, 22, 6, 29, 955, DateTimeKind.Local).AddTicks(2727));

            migrationBuilder.UpdateData(
                table: "Recompensas",
                keyColumn: "Id",
                keyValue: 5,
                column: "FechaCreacion",
                value: new DateTime(2026, 5, 28, 22, 6, 29, 955, DateTimeKind.Local).AddTicks(2729));

            migrationBuilder.UpdateData(
                table: "Recompensas",
                keyColumn: "Id",
                keyValue: 6,
                column: "FechaCreacion",
                value: new DateTime(2026, 5, 28, 22, 6, 29, 955, DateTimeKind.Local).AddTicks(2731));

            migrationBuilder.UpdateData(
                table: "Recompensas",
                keyColumn: "Id",
                keyValue: 7,
                column: "FechaCreacion",
                value: new DateTime(2026, 5, 28, 22, 6, 29, 955, DateTimeKind.Local).AddTicks(2733));

            migrationBuilder.UpdateData(
                table: "Recompensas",
                keyColumn: "Id",
                keyValue: 8,
                column: "FechaCreacion",
                value: new DateTime(2026, 5, 28, 22, 6, 29, 955, DateTimeKind.Local).AddTicks(2735));

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 100,
                column: "FechaRegistro",
                value: new DateTime(2026, 5, 28, 22, 6, 29, 950, DateTimeKind.Local).AddTicks(642));

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 200,
                column: "FechaRegistro",
                value: new DateTime(2026, 5, 28, 22, 6, 29, 953, DateTimeKind.Local).AddTicks(3191));
        }
    }
}
