FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY JobAlert.sln .
COPY src/Api/Api.csproj ./src/Api/Api.csproj
COPY src/Shared/Shared.csproj ./src/Shared/Shared.csproj
COPY src/Worker/Worker.csproj ./src/Worker/Worker.csproj
RUN dotnet restore

# Copy everything else and build
COPY . .
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "Api.dll"]
