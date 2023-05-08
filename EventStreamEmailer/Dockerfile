FROM mcr.microsoft.com/dotnet/sdk:7.0 AS base
WORKDIR /app

# Copy source and build
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["EventStreamEmailer.csproj", "EventStreamEmailer/"]
RUN dotnet restore "EventStreamEmailer/EventStreamEmailer.csproj"
COPY . "EventStreamEmailer/"
WORKDIR "/src/EventStreamEmailer"
RUN dotnet build "EventStreamEmailer.csproj" -c Release -o /app

# Publish
FROM build AS publish
RUN dotnet publish "EventStreamEmailer.csproj" -c Release -o /app

# Run
FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "EventStreamEmailer.dll"]