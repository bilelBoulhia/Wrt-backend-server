# Base stage for ASP.NET runtime and Redis installation
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081
EXPOSE 6379

# Install Redis as root
RUN apt-get update && apt-get install -y redis-server

# Switch to non-root user
USER $APP_UID

# Build stage for compiling and publishing the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["WrtWebSocketServer.csproj", "."]
RUN dotnet restore "./WrtWebSocketServer.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "./WrtWebSocketServer.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish stage for final application binaries
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./WrtWebSocketServer.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final stage to run the application
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Start Redis and your application
ENTRYPOINT redis-server --protected-mode no --bind 0.0.0.0  & dotnet WrtWebSocketServer.dll




## See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.
#
## This stage is used when running from VS in fast mode (Default for Debug configuration)
#FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
#USER $APP_UID
#WORKDIR /app
#EXPOSE 8080
#EXPOSE 8081
#
#
## This stage is used to build the service project
#FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
#ARG BUILD_CONFIGURATION=Release
#WORKDIR /src
#COPY ["WrtWebSocketServer/WrtWebSocketServer.csproj", "WrtWebSocketServer/"]
#RUN dotnet restore "./WrtWebSocketServer/WrtWebSocketServer.csproj"
#COPY . .
#WORKDIR "/src/WrtWebSocketServer"
#RUN dotnet build "./WrtWebSocketServer.csproj" -c $BUILD_CONFIGURATION -o /app/build
#
## This stage is used to publish the service project to be copied to the final stage
#FROM build AS publish
#ARG BUILD_CONFIGURATION=Release
#RUN dotnet publish "./WrtWebSocketServer.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false
#
## This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "WrtWebSocketServer.dll"]
