# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY json-vb.sln ./
COPY API/API.vbproj ./API/
COPY JsonParser/JsonParser.vbproj ./JsonParser/
RUN dotnet restore "API/API.vbproj"

COPY API/. ./API/
COPY JsonParser/. ./JsonParser/
RUN dotnet publish "API/API.vbproj" -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS final
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8000
ENV ASPNETCORE_URLS=http://0.0.0.0:8000
ENTRYPOINT ["dotnet", "API.dll"]
