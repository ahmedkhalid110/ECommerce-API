# Base image for running the app
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Build image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/ECommerce.API/ECommerce.API.csproj", "ECommerce.API/"]
# إذا كان لديك مشاريع أخرى في Clean Architecture (مثل Core, Infrastructure, Application) أضفها هنا:
# COPY ["src/ECommerce.Core/ECommerce.Core.csproj", "ECommerce.Core/"]
# COPY ["src/ECommerce.Application/ECommerce.Application.csproj", "ECommerce.Application/"]
# COPY ["src/ECommerce.Infrastructure/ECommerce.Infrastructure.csproj", "ECommerce.Infrastructure/"]

RUN dotnet restore "ECommerce.API/ECommerce.API.csproj"
COPY src/ .
WORKDIR "/src/ECommerce.API"
RUN dotnet build "ECommerce.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ECommerce.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ECommerce.API.dll"]