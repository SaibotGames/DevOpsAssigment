FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
ARG APP_UID=1000
# USER $APP_UID
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy project files
COPY BlazorApp1/BlazorApp1.csproj BlazorApp1/
COPY dto/dto.csproj dto/

# Restore dependencies
RUN dotnet restore BlazorApp1/BlazorApp1.csproj

# Copy all source code
COPY . .

# Build the project
RUN dotnet build BlazorApp1/BlazorApp1.csproj -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish BlazorApp1/BlazorApp1.csproj -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BlazorApp1.dll"]
