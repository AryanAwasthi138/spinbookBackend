# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy everything and restore
COPY *.sln .
COPY . ./
RUN dotnet restore
RUN dotnet publish -c Release -o /app

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app .

# Use the port Render expects
ENV ASPNETCORE_URLS=http://0.0.0.0:8080
EXPOSE 8080

# Run your backend
ENTRYPOINT ["dotnet", "TT_Exp.dll"]
