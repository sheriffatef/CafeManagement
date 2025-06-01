FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copy all csproj files and restore as distinct layers to cache the restore step
COPY ["CafeManagement/CafeManagement.csproj", "CafeManagement/"]
COPY ["DomainLayer/DomainLayer.csproj", "DomainLayer/"]
COPY ["Persistence/Persistence.csproj", "Persistence/"]
COPY ["Presentation/Presentation.csproj", "Presentation/"]
COPY ["Service/Service.csproj", "Service/"]
COPY ["ServiceAbstraction/ServiceAbstraction.csproj", "ServiceAbstraction/"]
COPY ["sHARED/Shared.csproj", "sHARED/"]

RUN dotnet restore "CafeManagement/CafeManagement.csproj"

# Copy everything else and build the application
COPY . .
WORKDIR "/src/CafeManagement"
RUN dotnet build "CafeManagement.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CafeManagement.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Set environment variables for Railway
ENV ASPNETCORE_URLS=http://+:$PORT
ENV ASPNETCORE_ENVIRONMENT=Production

ENTRYPOINT ["dotnet", "CafeManagement.dll"]
