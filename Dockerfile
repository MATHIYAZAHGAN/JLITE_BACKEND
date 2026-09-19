# Use the official .NET SDK image for building
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy project file and restore dependencies
COPY ["JLITE.API.csproj", "./"]
RUN dotnet restore "JLITE.API.csproj"

# Copy all source files
COPY . .

# Build the application
RUN dotnet build "JLITE.API.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "JLITE.API.csproj" -c Release -o /app/publish

# Use the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Copy published files
COPY --from=publish /app/publish .

# Expose port 8080 (Cloud Run uses this by default)
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

# Run the application
ENTRYPOINT ["dotnet", "JLITE.API.dll"]
