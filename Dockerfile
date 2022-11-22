#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM metrol/cmake:6.0-jammy AS build
WORKDIR /src
COPY . .

WORKDIR "/src/Console"

RUN dotnet build -p:Platform=x64 "Console.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -p:Platform=x64 "Console.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT [ "dotnet", "Console.dll" ]