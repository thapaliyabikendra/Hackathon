FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["NuGet.Config", "."]
COPY ["src/Hackathon.HttpApi.Host/Hackathon.HttpApi.Host.csproj", "src/Hackathon.HttpApi.Host/"]
COPY ["src/Hackathon.Application/Hackathon.Application.csproj", "src/Hackathon.Application/"]
COPY ["src/Hackathon.Application.Contracts/Hackathon.Application.Contracts.csproj", "src/Hackathon.Application.Contracts/"]
COPY ["src/Hackathon.Domain.Shared/Hackathon.Domain.Shared.csproj", "src/Hackathon.Domain.Shared/"]
COPY ["src/Hackathon.Domain/Hackathon.Domain.csproj", "src/Hackathon.Domain/"]
COPY ["src/Hackathon.EntityFrameworkCore/Hackathon.EntityFrameworkCore.csproj", "src/Hackathon.EntityFrameworkCore/"]
COPY ["src/Hackathon.HttpApi/Hackathon.HttpApi.csproj", "src/Hackathon.HttpApi/"]
RUN dotnet restore "src/Hackathon.HttpApi.Host/Hackathon.HttpApi.Host.csproj"
COPY . .
WORKDIR "/src/src/Hackathon.HttpApi.Host"
RUN dotnet build "Hackathon.HttpApi.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Hackathon.HttpApi.Host.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Hackathon.HttpApi.Host.dll"]