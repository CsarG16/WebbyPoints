using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebbyPoints.Migrations
{
    /// <inheritdoc />
    public partial class Gamificacion_Recompensas_Canjes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "EsEventoEspecial",
                table: "PuntosInteres",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "PuntosRecompensa",
                table: "PuntosInteres",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Recompensas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    Descripcion = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    CostoPuntos = table.Column<int>(type: "INTEGER", nullable: false),
                    Stock = table.Column<int>(type: "INTEGER", nullable: false),
                    Categoria = table.Column<string>(type: "TEXT", nullable: false),
                    ImagenUrl = table.Column<string>(type: "TEXT", nullable: true),
                    Activa = table.Column<bool>(type: "INTEGER", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recompensas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Canjes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UsuarioId = table.Column<int>(type: "INTEGER", nullable: false),
                    RecompensaId = table.Column<int>(type: "INTEGER", nullable: false),
                    FechaCanje = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CodigoVoucher = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Estado = table.Column<string>(type: "TEXT", nullable: false),
                    PuntosGastados = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Canjes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Canjes_Recompensas_RecompensaId",
                        column: x => x.RecompensaId,
                        principalTable: "Recompensas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Canjes_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "PuntosInteres",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EsEventoEspecial", "PuntosRecompensa" },
                values: new object[] { false, 10 });

            migrationBuilder.UpdateData(
                table: "PuntosInteres",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "EsEventoEspecial", "PuntosRecompensa" },
                values: new object[] { false, 10 });

            migrationBuilder.UpdateData(
                table: "PuntosInteres",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "EsEventoEspecial", "PuntosRecompensa" },
                values: new object[] { false, 10 });

            migrationBuilder.UpdateData(
                table: "PuntosInteres",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "EsEventoEspecial", "PuntosRecompensa" },
                values: new object[] { false, 10 });

            migrationBuilder.UpdateData(
                table: "PuntosInteres",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "EsEventoEspecial", "PuntosRecompensa" },
                values: new object[] { false, 10 });

            migrationBuilder.UpdateData(
                table: "PuntosInteres",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "EsEventoEspecial", "PuntosRecompensa" },
                values: new object[] { false, 10 });

            migrationBuilder.InsertData(
                table: "PuntosInteres",
                columns: new[] { "Id", "Calificacion", "Categoria", "Descripcion", "Distancia", "EsEventoEspecial", "Etiquetas", "Horario", "ImagenUrl", "Nombre", "PlanTipo", "PrecioRango", "PuntosRecompensa", "Ubicacion" },
                values: new object[,]
                {
                    { 7, 4.9000000000000004, "Medio Ambiente", "Únete a la jornada ecológica de limpieza en la Costa Verde. Contribuye al medio ambiente y gana puntos extra.", 0.0, true, "", "07:00 AM - 01:00 PM", "https://images.unsplash.com/photo-1618477460930-d8c0465e5744", "Limpieza de Playas - Costa Verde", "Amigos", "S/", 25, "Playa Costa Verde, Miraflores" },
                    { 8, 4.7000000000000002, "Cultura", "Recorre las galerías del MALI con entrada libre para universitarios. Arte contemporáneo y clásico peruano.", 0.0, true, "", "06:00 PM - 11:00 PM", "https://images.unsplash.com/photo-1554907984-15263bfd63bd", "Noche de Museos - Museo de Arte de Lima", "Pareja", "S/", 25, "Parque de la Exposición, Cercado de Lima" },
                    { 9, 5.0, "Cívico", "Ayuda en la preparación y distribución de alimentos para familias vulnerables en Lima Norte.", 0.0, true, "", "08:00 AM - 02:00 PM", "https://images.unsplash.com/photo-1593113598332-cd288d649433", "Voluntariado - Comedor Popular San Martín", "Amigos", "S/", 25, "Jr. San Martín 450, Los Olivos" },
                    { 10, 4.7999999999999998, "Medio Ambiente", "Participa en la reforestación urbana plantando árboles nativos en el Parque Ecológico de Villa El Salvador.", 0.0, true, "", "07:00 AM - 12:00 PM", "https://images.unsplash.com/photo-1542601906990-b4d3fb778b09", "Siembra de Árboles - Parque Ecológico", "Amigos", "S/", 25, "Parque Ecológico, Villa El Salvador" }
                });

            migrationBuilder.InsertData(
                table: "Recompensas",
                columns: new[] { "Id", "Activa", "Categoria", "CostoPuntos", "Descripcion", "FechaCreacion", "ImagenUrl", "Nombre", "Stock" },
                values: new object[,]
                {
                    { 1, true, "Descuento", 50, "Un café de cualquier tamaño en el Starbucks frente a la sede de Santa Anita. Válido por 7 días.", new DateTime(2026, 5, 28, 22, 6, 29, 955, DateTimeKind.Local).AddTicks(172), "https://images.unsplash.com/photo-1509042239860-f550ce710b93", "Café Gratis en Starbucks USMP", 100 },
                    { 2, true, "Descuento", 80, "Descuento del 15% en tu cuenta total. Presentar voucher al mesero. Válido por 14 días.", new DateTime(2026, 5, 28, 22, 6, 29, 955, DateTimeKind.Local).AddTicks(2718), "https://images.unsplash.com/photo-1513104890138-7c749659a591", "15% de Descuento en Pizza & Chill", 50 },
                    { 3, true, "Ocio", 150, "Dos entradas para cualquier función de lunes a jueves. Válido por 30 días.", new DateTime(2026, 5, 28, 22, 6, 29, 955, DateTimeKind.Local).AddTicks(2725), "https://images.unsplash.com/photo-1485846234645-a62644f84728", "Entrada Doble al Cine Planetario", 30 },
                    { 4, true, "Merchandising", 200, "Termo de acero inoxidable de 500ml con el logo de la USMP. Edición limitada.", new DateTime(2026, 5, 28, 22, 6, 29, 955, DateTimeKind.Local).AddTicks(2727), "https://images.unsplash.com/photo-1602143407151-7111542de6e8", "Termo Coleccionable USMP", 20 },
                    { 5, true, "Experiencia", 120, "Entrada gratuita al Circuito Mágico del Agua para ti y un acompañante. Válido fines de semana.", new DateTime(2026, 5, 28, 22, 6, 29, 955, DateTimeKind.Local).AddTicks(2729), "https://images.unsplash.com/photo-1504214208698-ea1916a2195a", "Pase Libre - Parque de las Aguas", 40 },
                    { 6, true, "Merchandising", 300, "Polera de algodón premium con el diseño exclusivo de WebbyPoints. Tallas S, M, L, XL.", new DateTime(2026, 5, 28, 22, 6, 29, 955, DateTimeKind.Local).AddTicks(2731), "https://images.unsplash.com/photo-1521572163474-6864f9cf17ab", "Polera Oficial WebbyPoints", 15 },
                    { 7, true, "Experiencia", 400, "Recorrido guiado por 4 restaurantes emblemáticos de Barranco con degustación incluida.", new DateTime(2026, 5, 28, 22, 6, 29, 955, DateTimeKind.Local).AddTicks(2733), "https://images.unsplash.com/photo-1414235077428-338989a2e8c0", "Tour Gastronómico por Barranco", 10 },
                    { 8, true, "Cultura", 180, "Acceso VIP al Museo de Arte de Lima con guía personalizado para ti y 2 amigos.", new DateTime(2026, 5, 28, 22, 6, 29, 955, DateTimeKind.Local).AddTicks(2735), "https://images.unsplash.com/photo-1554907984-15263bfd63bd", "Entrada al Museo MALI + Guía", 25 }
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Canjes_CodigoVoucher",
                table: "Canjes",
                column: "CodigoVoucher",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Canjes_RecompensaId",
                table: "Canjes",
                column: "RecompensaId");

            migrationBuilder.CreateIndex(
                name: "IX_Canjes_UsuarioId",
                table: "Canjes",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Canjes");

            migrationBuilder.DropTable(
                name: "Recompensas");

            migrationBuilder.DeleteData(
                table: "PuntosInteres",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "PuntosInteres",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "PuntosInteres",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "PuntosInteres",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DropColumn(
                name: "EsEventoEspecial",
                table: "PuntosInteres");

            migrationBuilder.DropColumn(
                name: "PuntosRecompensa",
                table: "PuntosInteres");

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
        }
    }
}
