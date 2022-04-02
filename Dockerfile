#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/Yande.Api/Yande.Api.csproj", "src/Yande.Api/"]
COPY ["src/Yande.Middleware/Yande.Middleware.csproj", "src/Yande.Middleware/"]
COPY ["src/Yande.Core.Package/Yande.Core.Package.csproj", "src/Yande.Core.Package/"]
COPY ["src/Yande.Core.Service/Yande.Core.Service.csproj", "src/Yande.Core.Service/"]
COPY ["src/Yande.Core.Redis/Yande.Core.Redis.csproj", "src/Yande.Core.Redis/"]
COPY ["src/Yande.Core.Entity/Yande.Core.Entity.csproj", "src/Yande.Core.Entity/"]
COPY ["src/Yande.Core.AppSettings/Yande.Core.AppSettings.csproj", "src/Yande.Core.AppSettings/"]
COPY ["src/Yande.Core.Filter/Yande.Core.Filter.csproj", "src/Yande.Core.Filter/"]
RUN dotnet restore "src/Yande.Api/Yande.Api.csproj"
COPY . .
WORKDIR "/src/src/Yande.Api"
RUN dotnet build "Yande.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Yande.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Yande.Api.dll"]