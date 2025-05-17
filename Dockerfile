# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy project files first for caching
COPY ["AnimalShelter/AnimalShelter.csproj", "AnimalShelter/"]
RUN dotnet restore "AnimalShelter/AnimalShelter.csproj"

# Copy everything else
COPY . .

# Build and publish
WORKDIR "/src/AnimalShelter"
RUN dotnet publish -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "AnimalShelter.dll"]