# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy solution and projects
COPY DevVault.sln ./
COPY src/DevVault.Api/DevVault.Api.csproj src/DevVault.Api/
COPY src/DevVault.Application/DevVault.Application.csproj src/DevVault.Application/
COPY src/DevVault.Domain/DevVault.Domain.csproj src/DevVault.Domain/
COPY src/DevVault.Infrastructure/DevVault.Infrastructure.csproj src/DevVault.Infrastructure/

# Restore all projects
RUN dotnet restore

# Copy the rest of the code
COPY src/ ./src/

# Publish the API
RUN dotnet publish src/DevVault.Api/DevVault.Api.csproj -c Release -o /app

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app ./

EXPOSE 8080
ENTRYPOINT ["dotnet", "DevVault.Api.dll"]
