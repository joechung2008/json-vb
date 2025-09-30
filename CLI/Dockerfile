# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY json-vb.sln ./
COPY CLI/CLI.vbproj ./CLI/
COPY JsonParser/JsonParser.vbproj ./JsonParser/
RUN dotnet restore "CLI/CLI.vbproj"

COPY CLI/. ./CLI/
COPY JsonParser/. ./JsonParser/
RUN dotnet publish "CLI/CLI.vbproj" -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/runtime:8.0-alpine AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "CLI.dll"]
