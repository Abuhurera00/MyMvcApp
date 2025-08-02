# ==========================
# 1) Build stage
# ==========================
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copy only the csproj first, restore dependencies, then copy the rest
COPY ["MyMvcApp.csproj", "./"]
RUN dotnet restore "MyMvcApp.csproj"

COPY . ./
RUN dotnet publish "MyMvcApp.csproj" \
    -c Release \
    -o /app/publish

# ==========================
# 2) Runtime stage
# ==========================
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app

# Copy the built app from the build stage
COPY --from=build /app/publish ./

# Expose port 80 (container port)
EXPOSE 80

# Optional: ensure the app uses Production settings by default
ENV ASPNETCORE_ENVIRONMENT=Production

# Entrypoint
ENTRYPOINT ["dotnet", "MyMvcApp.dll"]
