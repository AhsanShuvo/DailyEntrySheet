# https://hub.docker.com/_/microsoft-dotnet-core-sdk/
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build

# Set the working directory
WORKDIR /source

# Copy csproj and restore as distinct layers
COPY *.sln .
COPY EntrySheet.Web/*.csproj ./EntrySheet.Web/
RUN dotnet restore

# Copy everything else and build app
COPY EntrySheet.Web/. ./EntrySheet.Web/
WORKDIR /source/EntrySheet.Web
RUN dotnet publish -c release -o /app --no-restore

# https://hub.docker.com/_/microsoft-dotnet-core-aspnet/
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "EntrySheet.Web.dll"]
