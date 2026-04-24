# Proyecto de Programación I - WebbyPoints

## Requerimientos de Software
* **SDK de .NET:** 10.0
* **IDE:** VS Code o Visual Studio 2022
* **Base de Datos:** SQLite (No requiere instalación de servidor)

## Dependencias (NuGet)
* Microsoft.EntityFrameworkCore.Sqlite
* Microsoft.EntityFrameworkCore.Design

## Pasos para ejecutar
1. Clonar el repositorio.
2. Ejecutar `dotnet restore` para instalar dependencias.
3. Ejecutar `dotnet ef database update` para crear la base de datos local.
4. `dotnet run` para iniciar.