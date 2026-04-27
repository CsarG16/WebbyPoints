CREATE TABLE "PuntosInteres" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_PuntosInteres" PRIMARY KEY AUTOINCREMENT,
    "Nombre" TEXT NOT NULL,
    "Descripcion" TEXT NOT NULL,
    "Categoria" TEXT NOT NULL,
    "Ubicacion" TEXT NOT NULL,
    "ImagenUrl" TEXT NULL,
    "PlanTipo" TEXT NOT NULL,
    "Calificacion" REAL NOT NULL,
    "Distancia" REAL NOT NULL,
    "PrecioRango" TEXT NOT NULL,
    "Etiquetas" TEXT NOT NULL,
    "Horario" TEXT NOT NULL
);


CREATE TABLE "Usuarios" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Usuarios" PRIMARY KEY AUTOINCREMENT,
    "Nombre" TEXT NOT NULL,
    "Email" TEXT NOT NULL,
    "Password" TEXT NOT NULL,
    "Universidad" TEXT NOT NULL,
    "Carrera" TEXT NOT NULL,
    "Edad" INTEGER NOT NULL,
    "Preferencias" TEXT NOT NULL,
    "FotoUrl" TEXT NULL,
    "FechaRegistro" TEXT NOT NULL,
    "Puntos" INTEGER NOT NULL,
    "Rango" TEXT NOT NULL,
    "EsAdmin" INTEGER NOT NULL
);


CREATE TABLE "Reseñas" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Reseñas" PRIMARY KEY AUTOINCREMENT,
    "Comentario" TEXT NOT NULL,
    "Calificacion" INTEGER NOT NULL,
    "FechaPublicacion" TEXT NOT NULL,
    "PuntoInteresId" INTEGER NOT NULL,
    "UsuarioId" INTEGER NOT NULL,
    CONSTRAINT "FK_Reseñas_PuntosInteres_PuntoInteresId" FOREIGN KEY ("PuntoInteresId") REFERENCES "PuntosInteres" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_Reseñas_Usuarios_UsuarioId" FOREIGN KEY ("UsuarioId") REFERENCES "Usuarios" ("Id") ON DELETE CASCADE
);


INSERT INTO "PuntosInteres" ("Id", "Calificacion", "Categoria", "Descripcion", "Distancia", "Etiquetas", "Horario", "ImagenUrl", "Nombre", "PlanTipo", "PrecioRango", "Ubicacion")
VALUES (1, 4.5, 'Estudio', 'El lugar ideal para concentrarse con un buen café y WiFi estable.', 0.0, '', '07:00 AM - 10:00 PM', 'https://images.unsplash.com/photo-1509042239860-f550ce710b93', 'Starbucks Central', 'Solo', 'S/', 'Av. Principal 123');
SELECT changes();

INSERT INTO "PuntosInteres" ("Id", "Calificacion", "Categoria", "Descripcion", "Distancia", "Etiquetas", "Horario", "ImagenUrl", "Nombre", "PlanTipo", "PrecioRango", "Ubicacion")
VALUES (2, 4.7999999999999998, 'Ocio', 'Bar con ambiente relajado, perfecto para un after-class.', 0.0, '', '04:00 PM - 02:00 AM', '/images/bodega_esquina.png', 'La Bodega de la Esquina', 'Amigos', 'S/', 'Calle Universitaria 456');
SELECT changes();

INSERT INTO "PuntosInteres" ("Id", "Calificacion", "Categoria", "Descripcion", "Distancia", "Etiquetas", "Horario", "ImagenUrl", "Nombre", "PlanTipo", "PrecioRango", "Ubicacion")
VALUES (3, 4.9000000000000004, 'Romántico', 'Vista increíble y ambiente tranquilo para una cita especial.', 0.0, '', '06:00 AM - 11:00 PM', 'https://images.unsplash.com/photo-1529619768328-e37af76c6fe5', 'Mirador del Parque', 'Pareja', 'S/', 'Malecón de los Estudiantes');
SELECT changes();

INSERT INTO "PuntosInteres" ("Id", "Calificacion", "Categoria", "Descripcion", "Distancia", "Etiquetas", "Horario", "ImagenUrl", "Nombre", "PlanTipo", "PrecioRango", "Ubicacion")
VALUES (4, 4.2000000000000002, 'Estudio', 'Silencio absoluto y miles de libros a tu disposición.', 0.0, '', '08:00 AM - 08:00 PM', 'https://images.unsplash.com/photo-1521587760476-6c12a4b040da', 'Biblioteca Nacional (Sede Norte)', 'Solo', 'S/', 'Av. Cultura 789');
SELECT changes();

INSERT INTO "PuntosInteres" ("Id", "Calificacion", "Categoria", "Descripcion", "Distancia", "Etiquetas", "Horario", "ImagenUrl", "Nombre", "PlanTipo", "PrecioRango", "Ubicacion")
VALUES (5, 4.5999999999999996, 'Comida', 'Las mejores pizzas artesanales para compartir.', 0.0, '', '12:00 PM - 11:00 PM', 'https://images.unsplash.com/photo-1513104890138-7c749659a591', 'Pizza & Chill', 'Amigos', 'S/', 'Pasaje Gastronómico 10');
SELECT changes();

INSERT INTO "PuntosInteres" ("Id", "Calificacion", "Categoria", "Descripcion", "Distancia", "Etiquetas", "Horario", "ImagenUrl", "Nombre", "PlanTipo", "PrecioRango", "Ubicacion")
VALUES (6, 4.4000000000000004, 'Ocio', 'Estrenos con descuento para universitarios.', 0.0, '', '02:00 PM - 12:00 AM', 'https://images.unsplash.com/photo-1485846234645-a62644f84728', 'Cine Planetario', 'Pareja', 'S/', 'Centro Comercial El Polo');
SELECT changes();



INSERT INTO "Usuarios" ("Id", "Carrera", "Edad", "Email", "EsAdmin", "FechaRegistro", "FotoUrl", "Nombre", "Password", "Preferencias", "Puntos", "Rango", "Universidad")
VALUES (100, 'Ingeniería de Computación y Sistemas', 21, 'cesar_paredes7@usmp.pe', 1, '2026-04-26 23:36:10.9256108', NULL, 'César Paredes', 'Vinicola14//', 'Todo', 999, 'Administrador', 'USMP');
SELECT changes();

INSERT INTO "Usuarios" ("Id", "Carrera", "Edad", "Email", "EsAdmin", "FechaRegistro", "FotoUrl", "Nombre", "Password", "Preferencias", "Puntos", "Rango", "Universidad")
VALUES (101, 'Ingeniería de Computación y Sistemas', 21, 'fausto_miranda@usmp.pe', 1, '2026-04-26 23:36:10.9272839', NULL, 'Fausto Miranda', 'Mimamamemima123', 'Todo', 999, 'Administrador', 'USMP');
SELECT changes();



CREATE INDEX "IX_Reseñas_PuntoInteresId" ON "Reseñas" ("PuntoInteresId");


CREATE INDEX "IX_Reseñas_UsuarioId" ON "Reseñas" ("UsuarioId");


