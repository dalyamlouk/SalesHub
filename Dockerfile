FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base

WORKDIR /app

EXPOSE 8050

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

WORKDIR /src


COPY ["SalesHub.Api.csproj", "."]


RUN dotnet restore ".

COPY bin/Debug/net7.0/publish .

FROM base AS final

COPY --from=build /src/bin/Debug/net7.0/publish .

ENTRYPOINT ["dotnet", "SalesHub.Api.dll"]