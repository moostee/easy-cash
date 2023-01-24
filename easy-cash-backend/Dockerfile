FROM mcr.microsoft.com/dotnet/aspnet:6.0 as base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY ["EasyCash.Api/EasyCash.Api.csproj", "EasyCash.Api/"]
COPY ["EasyCash.Application/EasyCash.Application.csproj", "EasyCash.Application/"]
COPY ["EasyCash.Domain/EasyCash.Domain.csproj", "EasyCash.Domain/"]
COPY ["EasyCash.Infrastructure/EasyCash.Infrastructure.csproj", "EasyCash.Infrastructure/"]
COPY ["EasyCash.Shared/EasyCash.Shared.csproj", "EasyCash.Shared/"]

COPY . .
WORKDIR "/src/EasyCash.Api"

RUN dotnet build "EasyCash.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EasyCash.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EasyCash.Api.dll"]