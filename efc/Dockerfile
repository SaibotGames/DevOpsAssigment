FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["efc/efc.csproj", "efc/"]
RUN dotnet restore "efc/efc.csproj"
COPY . .
WORKDIR "/src/efc"
RUN dotnet build "efc.csproj" -c $BUILD_CONFIGURATION -o /app/build


# Install Entity Framework Core CLI tool
RUN dotnet tool install --global dotnet-ef 
ENV PATH="${PATH}:/root/.dotnet/tools"

RUN mkdir -p /app/data && chmod -R 777 /app/data
RUN dotnet ef --version  
RUN dotnet ef migrations add InitialMigration
RUN dotnet ef database update


FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "efc.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "efc.dll"]
