FROM mcr.microsoft.com/dotnet/sdk:9.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Install MySQL client and other tools needed for migrations
RUN apt-get update && apt-get install -y default-mysql-client bash && rm -rf /var/lib/apt/lists/*

# Install EF Core tools globally
RUN dotnet tool install --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
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

# Install EF Core tools in the build container
RUN dotnet tool install --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"

# Create migrations bundle
RUN dotnet ef migrations bundle --project "../Persistence/Persistence.csproj" --startup-project "CafeManagement.csproj" -o /app/publish/efbundle

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY entrypoint.sh /app/entrypoint.sh
RUN chmod +x /app/entrypoint.sh

# Set environment variables for Railway
# Railway will set $PORT automatically, default to 80 for local development
ENV PORT=80
ENV ASPNETCORE_URLS=http://+:${PORT}

# Enable debug mode
ENV ASPNETCORE_ENVIRONMENT=Development
ENV DOTNET_ENVIRONMENT=Development

# Enable detailed error messages
ENV ASPNETCORE_DETAILEDERRORS=true

# Enable developer exception page
ENV ASPNETCORE_SHOWEXCEPTIONDETAILS=true

# Enable console logging
ENV Logging__Console__LogLevel__Default=Debug
ENV Logging__Console__LogLevel__Microsoft=Debug

# MySQL Connection String and related environment variables
ENV ConnectionStrings__DefaultConnection="Server=mysql_host;Database=CafeManagement;User=admin;Password=admin;Port=3306;"
ENV MYSQL_HOST=mysql_host
ENV MYSQL_USER=admin
ENV MYSQL_PASSWORD=admin

# Use the entrypoint script to run migrations before starting the app
ENTRYPOINT ["/app/entrypoint.sh"]
