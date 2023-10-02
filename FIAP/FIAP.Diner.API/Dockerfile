FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["FIAP.Diner.API/FIAP.Diner.API.csproj", "FIAP.Diner.API/"]
COPY ["FIAP.Diner.Infrastructure/FIAP.Diner.Infrastructure.csproj", "FIAP.Diner.Infrastructure/"]
COPY ["FIAP.Diner.Domain/FIAP.Diner.Domain.csproj", "FIAP.Diner.Domain/"]
COPY ["FIAP.Diner.Application/FIAP.Diner.Application.csproj", "FIAP.Diner.Application/"]
RUN dotnet restore "FIAP.Diner.API/FIAP.Diner.API.csproj"
COPY . .
WORKDIR "/src/FIAP.Diner.API"
RUN dotnet build "FIAP.Diner.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FIAP.Diner.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FIAP.Diner.API.dll"]
