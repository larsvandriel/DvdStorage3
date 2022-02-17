#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["DvdStorage3Backend/DvdStorage3Backend.csproj", "DvdStorage3Backend/"]
RUN dotnet restore "DvdStorage3Backend/DvdStorage3Backend.csproj"
COPY . .
WORKDIR "/src/DvdStorage3Backend"
RUN dotnet build "DvdStorage3Backend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DvdStorage3Backend.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DvdStorage3Backend.dll"]