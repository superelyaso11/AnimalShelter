# Stage 1: Build the app
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy project files first to cache dependencies
COPY ["AnimalShelter/AnimalShelter.csproj", "AnimalShelter/"]
RUN dotnet restore "AnimalShelter/AnimalShelter.csproj"

# Copy everything else
COPY . .
WORKDIR "/src/AnimalShelter"

# Build and publish
RUN dotnet publish -c Release -o /app/publish

# Stage 2: Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app

# Copy published files
COPY --from=build /app/publish .

# Set environment variables for MySQL
ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS=http://*:$PORT

# Run migrations and start the app
ENTRYPOINT ["dotnet", "AnimalShelter.dll"]