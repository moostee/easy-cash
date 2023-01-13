FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /src
COPY ["EasyCash.Api/EasyCash.Api.csproj", "EasyCash.Api/"]
COPY ["EasyCash.Application/EasyCash.Application.csproj", "EasyCash.Application/"]
COPY ["EasyCash.Domain/EasyCash.Domain.csproj", "EasyCash.Domain/"]
COPY ["EasyCash.Infrastructure/EasyCash.Infrastructure.csproj", "EasyCash.Infrastructure/"]
COPY ["EasyCash.Shared/EasyCash.Shared.csproj", "EasyCash.Shared/"]
RUN dotnet restore "EasyCash.Api/EasyCash.Api.csproj"
COPY . .
WORKDIR "/src/EasyCash.Api"
RUN dotnet build "EasyCash.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EasyCash.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EasyCash.Api.dll"]