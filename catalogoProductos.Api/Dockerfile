FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["catalogoProductos.Api/catalogoProductos.Api.csproj", "catalogoProductos.Api/"]
COPY ["catalogoProductos.Application/catalogoProductos.Application.csproj", "catalogoProductos.Application/"]
COPY ["catalogoProductos.Domain/catalogoProductos.Domain.csproj", "catalogoProductos.Domain/"]
COPY ["catalogoProductos.Infrastructure/catalogoProductos.Infrastructure.csproj", "catalogoProductos.Infrastructure/"]
RUN dotnet restore "catalogoProductos.Api/catalogoProductos.Api.csproj"
COPY . .
WORKDIR "/src/catalogoProductos.Api"
RUN dotnet build "./catalogoProductos.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./catalogoProductos.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "catalogoProductos.Api.dll"]
