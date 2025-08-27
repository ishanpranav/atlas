FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/Atlas.Server/Atlas.Server.csproj", "src/Atlas.Server/"]
RUN dotnet restore "./src/Atlas.Server/./Atlas.Server.csproj"
COPY . .
WORKDIR "/src/src/Atlas.Server"
RUN dotnet build "./Atlas.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "./Atlas.Server.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base as final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Atlas.Server.dll"]
