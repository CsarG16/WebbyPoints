using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebbyPoints.Migrations
{
    /// <inheritdoc />
    public partial class SeedPuntosInteres : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "PuntosInteres",
                columns: new[] { "Id", "Calificacion", "Categoria", "Descripcion", "Distancia", "Etiquetas", "Horario", "ImagenUrl", "Nombre", "PlanTipo", "PrecioRango", "Ubicacion" },
                values: new object[,]
                {
                    { 1, 4.5, "Estudio", "El lugar ideal para concentrarse con un buen café y WiFi estable.", 0.0, "", "07:00 AM - 10:00 PM", "https://images.unsplash.com/photo-1509042239860-f550ce710b93", "Starbucks Central", "Solo", "S/", "Av. Principal 123" },
                    { 2, 4.7999999999999998, "Ocio", "Bar con ambiente relajado, perfecto para un after-class.", 0.0, "", "04:00 PM - 02:00 AM", "https://images.unsplash.com/photo-1514933651103-005eec06c04b", "La Bodega de la Esquina", "Amigos", "S/", "Calle Universitaria 456" },
                    { 3, 4.9000000000000004, "Romántico", "Vista increíble y ambiente tranquilo para una cita especial.", 0.0, "", "06:00 AM - 11:00 PM", "https://images.unsplash.com/photo-1529619768328-e37af76c6fe5", "Mirador del Parque", "Pareja", "S/", "Malecón de los Estudiantes" },
                    { 4, 4.2000000000000002, "Estudio", "Silencio absoluto y miles de libros a tu disposición.", 0.0, "", "08:00 AM - 08:00 PM", "https://images.unsplash.com/photo-1521587760476-6c12a4b040da", "Biblioteca Nacional (Sede Norte)", "Solo", "S/", "Av. Cultura 789" },
                    { 5, 4.5999999999999996, "Comida", "Las mejores pizzas artesanales para compartir.", 0.0, "", "12:00 PM - 11:00 PM", "https://images.unsplash.com/photo-1513104890138-7c749659a591", "Pizza & Chill", "Amigos", "S/", "Pasaje Gastronómico 10" },
                    { 6, 4.4000000000000004, "Ocio", "Estrenos con descuento para universitarios.", 0.0, "", "02:00 PM - 12:00 AM", "https://images.unsplash.com/photo-1485846234645-a62644f84728", "Cine Planetario", "Pareja", "S/", "Centro Comercial El Polo" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PuntosInteres",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PuntosInteres",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PuntosInteres",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "PuntosInteres",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "PuntosInteres",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "PuntosInteres",
                keyColumn: "Id",
                keyValue: 6);
        }
    }
}
