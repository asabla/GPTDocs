FROM mcr.microsoft.com/dotnet/sdk:8.0 AS restore-env
WORKDIR /src

COPY ["./GPTDocs.API/GPTDocs.API.csproj", "./GPTDocs.API/"]

RUN dotnet restore "./GPTDocs.API/GPTDocs.API.csproj"

# Build stage
FROM restore-env AS build-env
WORKDIR /src

COPY --from=restore-env /src .
COPY ["./Directory.Packages.props", "./GPTDocs.API/*", "./GPTDocs.API/"]

RUN dotnet publish \
        --configuration Release \
        --runtime linux-x64 \
        --output ./app \
        "./GPTDocs.API/GPTDocs.API.csproj"

## Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime-env
WORKDIR /app
USER app

COPY --from=build-env /src/app .

EXPOSE 8080
EXPOSE 8081

ENTRYPOINT ["dotnet", "GPTDocs.API.dll"]
