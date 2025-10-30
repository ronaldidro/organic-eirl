FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["OrganicEIRL.API/OrganicEIRL.API.csproj", "OrganicEIRL.API/"]
COPY ["OrganicEIRL.Application/OrganicEIRL.Application.csproj", "OrganicEIRL.Application/"]
COPY ["OrganicEIRL.Domain/OrganicEIRL.Domain.csproj", "OrganicEIRL.Domain/"]
COPY ["OrganicEIRL.Infrastructure/OrganicEIRL.Infrastructure.csproj", "OrganicEIRL.Infrastructure/"]
RUN dotnet restore "OrganicEIRL.API/OrganicEIRL.API.csproj"
COPY . .
WORKDIR "/src/OrganicEIRL.API"
RUN dotnet build "OrganicEIRL.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OrganicEIRL.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OrganicEIRL.API.dll"]