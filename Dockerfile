# Build stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy csproj and restore as distinct layers
COPY ["WebbyPoints.csproj", "."]
RUN dotnet restore "./WebbyPoints.csproj"

# Copy everything else and build
COPY . .
RUN dotnet build "WebbyPoints.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "WebbyPoints.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Render uses a dynamic port, so we tell ASP.NET Core to listen on the port Render provides
ENV ASPNETCORE_URLS=http://+:10000

# Copy the database if it exists (Optional: usually you want a persistent disk or a separate DB)
# COPY WebbyPoints.db . 

ENTRYPOINT ["dotnet", "WebbyPoints.dll"]
