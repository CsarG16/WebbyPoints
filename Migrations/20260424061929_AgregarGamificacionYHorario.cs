using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebbyPoints.Migrations
{
    /// <inheritdoc />
    public partial class AgregarGamificacionYHorario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FechaRegistro",
                table: "Usuarios",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "FotoUrl",
                table: "Usuarios",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Puntos",
                table: "Usuarios",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Rango",
                table: "Usuarios",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaPublicacion",
                table: "Reseñas",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "Reseñas",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Horario",
                table: "PuntosInteres",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PlanTipo",
                table: "PuntosInteres",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Reseñas_UsuarioId",
                table: "Reseñas",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reseñas_Usuarios_UsuarioId",
                table: "Reseñas",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reseñas_Usuarios_UsuarioId",
                table: "Reseñas");

            migrationBuilder.DropIndex(
                name: "IX_Reseñas_UsuarioId",
                table: "Reseñas");

            migrationBuilder.DropColumn(
                name: "FechaRegistro",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "FotoUrl",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Puntos",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Rango",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "FechaPublicacion",
                table: "Reseñas");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Reseñas");

            migrationBuilder.DropColumn(
                name: "Horario",
                table: "PuntosInteres");

            migrationBuilder.DropColumn(
                name: "PlanTipo",
                table: "PuntosInteres");
        }
    }
}
