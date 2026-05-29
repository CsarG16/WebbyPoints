using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebbyPoints.Migrations
{
    /// <inheritdoc />
    public partial class SeedNewPlacesAndRewards : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "PuntosInteres",
                columns: new[] { "Id", "Calificacion", "Categoria", "Descripcion", "Distancia", "EsEventoEspecial", "Etiquetas", "Horario", "ImagenUrl", "Nombre", "PlanTipo", "PrecioRango", "PuntosRecompensa", "Ubicacion" },
                values: new object[,]
                {
                    { 11, 4.7000000000000002, "Cultura", "La feria literaria más importante del país. Cientos de stands, presentaciones de libros y conversatorios culturales.", 0.0, true, "", "11:00 AM - 10:00 PM", "https://images.unsplash.com/photo-1481627834876-b7833e8f5570", "Feria Internacional del Libro de Lima", "Amigos", "S/", 25, "Parque Próceres de la Independencia, Jesús María" },
                    { 12, 4.9000000000000004, "Comida", "Experiencia culinaria inigualable a cargo del chef Virgilio Martínez. Platos basados en insumos de diversas alturas geográficas.", 0.0, false, "", "12:45 PM - 11:00 PM", "https://images.unsplash.com/photo-1544025162-d76694265947", "Central Restaurante", "Pareja", "S/S/S/", 10, "Av. Pedro de Osma 301, Barranco" },
                    { 13, 4.5999999999999996, "Comida", "Los mejores sándwiches criollos tradicionales con papas nativas fritas y jugos naturales. Un sabor inconfundible.", 0.0, false, "", "07:00 AM - 01:00 AM", "https://images.unsplash.com/photo-1509722747041-616f39b57569", "La Lucha Sanguchería Criolla", "Solo", "S/", 10, "Pasaje Champagnat 139, Miraflores" },
                    { 14, 4.5, "Ocio", "Histórico bar limeño, reconocido como el lugar donde se perfeccionó la receta original del emblemático Pisco Sour.", 0.0, false, "", "12:00 PM - 10:00 PM", "https://images.unsplash.com/photo-1514362545857-3bc16c4c7d1b", "Bar Maury (Centro de Lima)", "Amigos", "S/S/", 10, "Jr. Carabaya 387, Centro de Lima" },
                    { 15, 4.7999999999999998, "Cultural", "El gran evento de cultura pop asiática en Lima. Concursos de cosplay, bandas en vivo, venta de merchandising y videojuegos.", 0.0, true, "", "10:00 AM - 09:00 PM", "https://images.unsplash.com/photo-1578632767115-351597cf2477", "Festival de Anime & Cosplay - Otaku Fest", "Amigos", "S/", 25, "Centro de Convenciones de Lima, San Borja" },
                    { 16, 4.9000000000000004, "Medio Ambiente", "Ayuda en el cuidado, alimentación y paseo de perros y gatos rescatados del abandono en este hermoso refugio animal.", 0.0, true, "", "09:00 AM - 04:00 PM", "https://images.unsplash.com/photo-1583511655857-d19b40a7a54e", "Voluntariado - Albergue 'Patitas Felices'", "Amigos", "S/", 25, "Calle Las Flores 450, Chorrillos" },
                    { 17, 5.0, "Cívico", "Comparte una tarde de juegos, lectura y conversación con personas de la tercera edad que necesitan compañía y afecto.", 0.0, true, "", "02:00 PM - 06:00 PM", "https://images.unsplash.com/photo-1576765608535-5f04d1e3f289", "Apoyo al Adulto Mayor - Asilo Canevaro", "Solo", "S/", 25, "Jr. Áncash 1400, Barrios Altos, Centro de Lima" },
                    { 18, 4.7000000000000002, "Comida", "Alta cocina peruana de autor en una hermosa casona histórica de San Isidro. Platos criollos reinterpretados de manera gourmet.", 0.0, false, "", "12:00 PM - 11:30 PM", "https://images.unsplash.com/photo-1414235077428-338989a2e8c0", "Astrid & Gastón", "Pareja", "S/S/S/", 10, "Av. Paz Soldán 290, San Isidro" },
                    { 19, 4.4000000000000004, "Estudio", "Discusión mensual de obras contemporáneas peruanas y latinoamericanas en un ambiente ameno y rodeado de libros.", 0.0, false, "", "04:00 PM - 07:00 PM", "https://images.unsplash.com/photo-1529156069898-49953e39b3ac", "Club de Lectura - Biblioteca Municipal", "Solo", "S/", 10, "Av. Larco 770, Miraflores" },
                    { 20, 4.5999999999999996, "Ocio", "Ven a jugar Catán, Dixit o Carcassonne con amigos o conoce gente nueva compartiendo juegos modernos de mesa.", 0.0, false, "", "02:00 PM - 10:00 PM", "https://images.unsplash.com/photo-1610890716171-6b1bb98ffd09", "Tarde de Juegos de Mesa - Portal de Ocio", "Amigos", "S/", 10, "Calle Cantuarias 140, Miraflores" },
                    { 21, 4.5, "Ocio", "Box privados para cantar a todo pulmón con una enorme selección de canciones y excelente servicio de coctelería y snacks.", 0.0, false, "", "06:00 PM - 03:00 AM", "https://images.unsplash.com/photo-1516280440614-37939bbacd6a", "Karaoke Sopranos Surco", "Amigos", "S/S/", 10, "Av. Primavera 1540, Santiago de Surco" },
                    { 22, 4.7999999999999998, "Comida", "Platos de antaño servidos en generosas porciones ideales para compartir en familia o con esa persona especial. El sabor de casa.", 0.0, false, "", "12:00 PM - 10:00 PM", "https://images.unsplash.com/photo-1555396273-367ea4eb4db5", "Isolina Taberna Peruana", "Pareja", "S/S/", 10, "Av. San Martín 101, Barranco" },
                    { 23, 4.7000000000000002, "Deporte", "El domingo por la mañana la Av. Arequipa se cierra al tráfico motorizado y se abre completamente para bicicletas, patines y caminatas.", 0.0, true, "", "07:00 AM - 01:00 PM", "https://images.unsplash.com/photo-1541614101331-1a5a3a194e92", "CicloDía - Av. Arequipa Libre", "Amigos", "S/", 25, "Av. Arequipa (Cdra 1 a 52), Lima" },
                    { 24, 4.5999999999999996, "Deporte", "Únete a grupos de corredores para recorrer el perímetro verde de la comandancia militar. Excelente ruta iluminada y segura.", 0.0, false, "", "05:00 AM - 10:00 PM", "https://images.unsplash.com/photo-1476480862126-209bfaa8edc8", "Running Club - El Pentagonito", "Solo", "S/", 10, "Av. San Borja Sur, San Borja" },
                    { 25, 4.7999999999999998, "Deporte", "Aprende a domar las olas con profesores calificados en la Costa Verde. Incluye tabla de surf y traje de neopreno.", 0.0, false, "", "06:00 AM - 05:00 PM", "https://images.unsplash.com/photo-1502680390469-be75c86b636f", "Clases de Surf en Playa Makaha", "Solo", "S/S/", 10, "Playa Makaha, Miraflores" },
                    { 26, 4.7999999999999998, "Romántico", "Parque de fuentes ornamentales con espectáculos multimedia interactivos de agua, luces y música. Un paseo romántico perfecto.", 0.0, false, "", "03:00 PM - 10:00 PM", "https://images.unsplash.com/photo-1504214208698-ea1916a2195a", "Circuito Mágico del Agua", "Pareja", "S/", 10, "Jr. Madre de Dios S/N, Cercado de Lima" },
                    { 27, 4.9000000000000004, "Romántico", "Vista hermosa de la bahía de Lima, murales de mosaicos coloridos y la icónica escultura de 'El Beso' de Víctor Delfín.", 0.0, false, "", "24 Horas", "https://images.unsplash.com/photo-1518199266791-5375a83190b7", "El Parque del Amor", "Pareja", "S/", 10, "Malecón Cisneros, Miraflores" },
                    { 28, 4.7000000000000002, "Estudio", "Una hermosa y pequeña cafetería-librería en Barranco donde puedes leer en silencio mientras disfrutas de un té artesanal.", 0.0, false, "", "09:00 AM - 09:00 PM", "https://images.unsplash.com/photo-1507842217343-583bb7270b66", "Café Cultural La Libre", "Solo", "S/", 10, "Av. San Martín 108, Barranco" },
                    { 29, 4.5, "Estudio", "Estudio académico con amplias salas de lectura silenciosa, computadoras de consulta y bibliotecología especializada.", 0.0, false, "", "08:00 AM - 09:00 PM", "https://images.unsplash.com/photo-1521587760476-6c12a4b040da", "Biblioteca de la USMP (Sede La Molina)", "Solo", "S/", 10, "Av. Los Corregidores 1150, La Molina" },
                    { 30, 4.2999999999999998, "Estudio", "Ubicación estratégica frente al campus universitario para hacer trabajos grupales, estudiar para parciales o tomar un espresso.", 0.0, false, "", "07:00 AM - 10:00 PM", "https://images.unsplash.com/photo-1509042239860-f550ce710b93", "Starbucks USMP (Sede Santa Anita)", "Solo", "S/", 10, "Av. Los Calamacos 450, Santa Anita" },
                    { 31, 4.9000000000000004, "Comida", "Feria gastronómica dentro del campus universitario. Expositores de comida selvática, marina y andina a precios de estudiante.", 0.0, true, "", "10:00 AM - 06:00 PM", "https://images.unsplash.com/photo-1504674900247-0877df9cc836", "Festival Mistura USMP (Feria Gastronómica)", "Amigos", "S/", 25, "Parque de la Exposición, Lima" },
                    { 32, 4.7999999999999998, "Cultura", "Espectacular casona virreinal que alberga una de las colecciones precolombinas de oro y plata más impresionantes del mundo.", 0.0, false, "", "09:00 AM - 07:00 PM", "https://images.unsplash.com/photo-1566121318599-8cf0c7921a8f", "Museo Larco", "Solo", "S/S/", 10, "Av. Simón Bolívar 1515, Pueblo Libre" },
                    { 33, 4.9000000000000004, "Cultura", "Disfruta de las mejores temporadas de ópera, ballet y conciertos de la Orquesta Sinfónica Nacional con acústica de primer nivel mundial.", 0.0, false, "", "07:30 PM - 10:00 PM", "https://images.unsplash.com/photo-1503095396549-807759245b35", "Gran Teatro Nacional (Sinfónica)", "Pareja", "S/S/", 10, "Av. Javier Prado Este 2225, San Borja" },
                    { 34, 5.0, "Cívico", "Sé un héroe cívico donando sangre para el banco nacional. Un acto altruista que salva hasta tres vidas humanas.", 0.0, true, "", "07:00 AM - 02:00 PM", "https://images.unsplash.com/photo-1615461066841-6116e61058f4", "Donación de Sangre - Hospital Rebagliati", "Solo", "S/", 25, "Av. Edgardo Rebagliati 490, Jesús María" },
                    { 35, 4.7999999999999998, "Medio Ambiente", "Sube las hermosas lomas costeras durante el invierno limeño para plantar semillas de flora nativa y proteger el ecosistema de la neblina.", 0.0, true, "", "07:00 AM - 01:00 PM", "https://images.unsplash.com/photo-1500382017468-9049fed747ef", "Reforestación - Lomas de Villa María", "Amigos", "S/", 25, "Lomas de Villa Maria del Triunfo, VMT" },
                    { 36, 4.9000000000000004, "Medio Ambiente", "Jornada cívica y ecológica para recolectar desechos plásticos en una de las playas más contaminadas del litoral del Callao.", 0.0, true, "", "08:00 AM - 02:00 PM", "https://images.unsplash.com/photo-1618477460930-d8c0465e5744", "Limpieza Extrema - Playa Carpayo", "Amigos", "S/", 25, "Playa Carpayo, Chucuito, Callao" },
                    { 37, 4.7000000000000002, "Ocio", "Impresionante bar de tragos de autor ubicado en una mansión restaurada del siglo XIX. Hermosa decoración barroca criolla.", 0.0, false, "", "06:00 PM - 02:00 AM", "https://images.unsplash.com/photo-1470337458703-46ad1756a187", "Bar Ayahuasca Barranco", "Amigos", "S/S/", 10, "Av. Prolongación San Martin 130, Barranco" },
                    { 38, 4.9000000000000004, "Romántico", "Disfruta de una cena espectacular a la luz de las velas frente al impresionante centro ceremonial precolombino iluminado.", 0.0, false, "", "07:00 PM - 11:30 PM", "https://images.unsplash.com/photo-1504674900247-0877df9cc836", "Huaca Pucllana (Cena Temática)", "Pareja", "S/S/S/", 10, "Calle General Borgoño 8ra cdra, Miraflores" },
                    { 39, 4.7999999999999998, "Ocio", "La convención de cultura pop geek más grande de Lima. Stands de Marvel, DC, ilustradores, actores internacionales invitados y cosplay.", 0.0, true, "", "11:00 AM - 09:00 PM", "https://images.unsplash.com/photo-1607604276583-eef5d076aa5f", "Comic Con Lima", "Amigos", "S/", 25, "Centro de Exposiciones Jockey, Surco" },
                    { 40, 4.7000000000000002, "Cultura", "Histórica y espaciosa librería limeña, ideal para tomar un café mientras ojeas las últimas novedades editoriales nacionales.", 0.0, false, "", "09:30 AM - 09:00 PM", "https://images.unsplash.com/photo-1512820790803-83ca734da794", "Librería El Virrey Miraflores", "Solo", "S/", 10, "Calle Bolognesi 510, Miraflores" }
                });

            migrationBuilder.UpdateData(
                table: "Recompensas",
                keyColumn: "Id",
                keyValue: 1,
                column: "FechaCreacion",
                value: new DateTime(2026, 5, 29, 12, 40, 22, 993, DateTimeKind.Local).AddTicks(427));

            migrationBuilder.UpdateData(
                table: "Recompensas",
                keyColumn: "Id",
                keyValue: 2,
                column: "FechaCreacion",
                value: new DateTime(2026, 5, 29, 12, 40, 22, 993, DateTimeKind.Local).AddTicks(2381));

            migrationBuilder.UpdateData(
                table: "Recompensas",
                keyColumn: "Id",
                keyValue: 3,
                column: "FechaCreacion",
                value: new DateTime(2026, 5, 29, 12, 40, 22, 993, DateTimeKind.Local).AddTicks(2387));

            migrationBuilder.UpdateData(
                table: "Recompensas",
                keyColumn: "Id",
                keyValue: 4,
                column: "FechaCreacion",
                value: new DateTime(2026, 5, 29, 12, 40, 22, 993, DateTimeKind.Local).AddTicks(2388));

            migrationBuilder.UpdateData(
                table: "Recompensas",
                keyColumn: "Id",
                keyValue: 5,
                column: "FechaCreacion",
                value: new DateTime(2026, 5, 29, 12, 40, 22, 993, DateTimeKind.Local).AddTicks(2389));

            migrationBuilder.UpdateData(
                table: "Recompensas",
                keyColumn: "Id",
                keyValue: 6,
                column: "FechaCreacion",
                value: new DateTime(2026, 5, 29, 12, 40, 22, 993, DateTimeKind.Local).AddTicks(2390));

            migrationBuilder.UpdateData(
                table: "Recompensas",
                keyColumn: "Id",
                keyValue: 7,
                column: "FechaCreacion",
                value: new DateTime(2026, 5, 29, 12, 40, 22, 993, DateTimeKind.Local).AddTicks(2391));

            migrationBuilder.UpdateData(
                table: "Recompensas",
                keyColumn: "Id",
                keyValue: 8,
                column: "FechaCreacion",
                value: new DateTime(2026, 5, 29, 12, 40, 22, 993, DateTimeKind.Local).AddTicks(2392));

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 100,
                columns: new[] { "Carrera", "FechaRegistro", "Preferencias", "Puntos" },
                values: new object[] { "Ingenieria de Sistemas", new DateTime(2026, 5, 29, 12, 40, 22, 990, DateTimeKind.Local).AddTicks(2303), "Estudio, Ocio, Comida, Cultura", 2000 });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 200,
                columns: new[] { "Carrera", "FechaRegistro", "Preferencias", "Puntos" },
                values: new object[] { "Ingenieria de Sistemas", new DateTime(2026, 5, 29, 12, 40, 22, 991, DateTimeKind.Local).AddTicks(9495), "Estudio, Ocio, Comida, Cultura", 1000 });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "Carrera", "Edad", "Email", "EsAdmin", "FechaRegistro", "FotoUrl", "Nombre", "Password", "Preferencias", "Puntos", "Rango", "Universidad" },
                values: new object[,]
                {
                    { 300, "Medicina", 20, "juan_perez@usmp.pe", false, new DateTime(2026, 5, 29, 12, 40, 22, 991, DateTimeKind.Local).AddTicks(9511), null, "Juan Pérez", "Juanito123//", "Comida, Parques", 500, "Explorador", "USMP" },
                    { 301, "Derecho", 22, "maria_rojas@usmp.pe", false, new DateTime(2026, 5, 29, 12, 40, 22, 991, DateTimeKind.Local).AddTicks(9513), null, "María Rojas", "Maria1234//", "Cafés, Estudiar", 500, "Explorador", "USMP" },
                    { 302, "Psicologia", 19, "luis_flores@usmp.pe", false, new DateTime(2026, 5, 29, 12, 40, 22, 991, DateTimeKind.Local).AddTicks(9514), null, "Luis Flores", "Luisito99//", "Gaming, Bares", 500, "Explorador", "USMP" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PuntosInteres",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "PuntosInteres",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "PuntosInteres",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "PuntosInteres",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "PuntosInteres",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "PuntosInteres",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "PuntosInteres",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "PuntosInteres",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "PuntosInteres",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "PuntosInteres",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "PuntosInteres",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "PuntosInteres",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "PuntosInteres",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "PuntosInteres",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "PuntosInteres",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "PuntosInteres",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "PuntosInteres",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "PuntosInteres",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "PuntosInteres",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "PuntosInteres",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "PuntosInteres",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "PuntosInteres",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "PuntosInteres",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "PuntosInteres",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "PuntosInteres",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "PuntosInteres",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "PuntosInteres",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "PuntosInteres",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "PuntosInteres",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "PuntosInteres",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 300);

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 301);

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 302);

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
                columns: new[] { "Carrera", "FechaRegistro", "Preferencias", "Puntos" },
                values: new object[] { "Ingeniería de Computación y Sistemas", new DateTime(2026, 5, 28, 22, 45, 1, 46, DateTimeKind.Local).AddTicks(6606), "Todo", 999 });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 200,
                columns: new[] { "Carrera", "FechaRegistro", "Preferencias", "Puntos" },
                values: new object[] { "Ingeniería de Computación y Sistemas", new DateTime(2026, 5, 28, 22, 45, 1, 48, DateTimeKind.Local).AddTicks(4491), "Todo", 999 });
        }
    }
}
