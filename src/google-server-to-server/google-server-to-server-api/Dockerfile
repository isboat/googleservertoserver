#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["google-server-to-server-api/google-server-to-server-api.csproj", "google-server-to-server-api/"]
RUN dotnet restore "google-server-to-server-api/google-server-to-server-api.csproj"
COPY . .
WORKDIR "/src/google-server-to-server-api"
RUN dotnet build "google-server-to-server-api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "google-server-to-server-api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "google-server-to-server-api.dll"]