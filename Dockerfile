# 1) Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

# 2) Build image
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copy csproj and restore
COPY ["MyMvcApp.csproj", "./"]
RUN dotnet restore "./MyMvcApp.csproj"

# Copy everything else and publish
COPY . .
RUN dotnet publish "MyMvcApp.csproj" -c Release -o /app/publish

# 3) Final image
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "MyMvcApp.dll"]
