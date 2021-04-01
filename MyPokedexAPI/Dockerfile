#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
EXPOSE 5000:5000

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY MyPokedexAPI.sln ./
COPY ["MyPokedexAPI/MyPokedex.API.csproj", "MyPokedexAPI/"]
COPY MyPokedex.ApplicationServices/MyPokedex.ApplicationServices.csproj MyPokedex.ApplicationServices/
COPY MyPokedex.Core/MyPokedex.Core.csproj MyPokedex.Core/
COPY MyPokedex.Infrastructure/MyPokedex.Infrastructure.csproj MyPokedex.Infrastructure/
RUN dotnet restore "MyPokedexAPI/MyPokedex.API.csproj"
COPY . .
WORKDIR "/src/MyPokedexAPI"
RUN dotnet build "../MyPokedexAPI.sln" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MyPokedex.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MyPokedex.API.dll"]