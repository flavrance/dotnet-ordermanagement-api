FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/OrderManagement.Api/OrderManagement.Api.csproj", "OrderManagement.Api/"]
COPY ["src/OrderManagement.Application/OrderManagement.Application.csproj", "OrderManagement.Application/"]
COPY ["src/OrderManagement.Domain/OrderManagement.Domain.csproj", "OrderManagement.Domain/"]
COPY ["src/OrderManagement.Infrastructure/OrderManagement.Infrastructure.csproj", "OrderManagement.Infrastructure/"]
RUN dotnet restore "OrderManagement.Api/OrderManagement.Api.csproj"
COPY src/ .
WORKDIR "/src/OrderManagement.Api"
RUN dotnet build "OrderManagement.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OrderManagement.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_ENVIRONMENT=Development
ENV ASPNETCORE_URLS=http://+:80
ENTRYPOINT ["dotnet", "OrderManagement.Api.dll"] 