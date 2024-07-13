#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["WebApi_BackEnd/WebApi_BackEnd.csproj", "WebApi_BackEnd/"]
COPY ["DataBase.Configuration/DataBase.Configuration.csproj", "DataBase.Configuration/"]
COPY ["Repository/Repository.csproj", "Repository/"]
COPY ["DataBase.Repository/DataBase.Repository.csproj", "DataBase.Repository/"]
COPY ["Logic.Service/Logic.Service.csproj", "Logic.Service/"]
COPY ["Utility/Utility.csproj", "Utility/"]
RUN dotnet restore "WebApi_BackEnd/WebApi_BackEnd.csproj"
COPY . .
WORKDIR "/src/WebApi_BackEnd"
RUN dotnet build "WebApi_BackEnd.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebApi_BackEnd.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app

COPY --from=publish /app/publish .
COPY ["WebApi_BackEnd/wwwroot" , "/app/wwwroot"] 
ENTRYPOINT ["dotnet", "WebApi_BackEnd.dll"]