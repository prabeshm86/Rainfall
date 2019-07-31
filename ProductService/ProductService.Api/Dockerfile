FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ProductService/ProductService.Api/ProductService.Api.csproj ProductService/ProductService.Api/
RUN dotnet restore "ProductService/ProductService.Api/ProductService.Api.csproj"
COPY . .
WORKDIR "/src/ProductService/ProductService.Api"
RUN dotnet build "ProductService.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "ProductService.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ProductService.Api.dll"]
