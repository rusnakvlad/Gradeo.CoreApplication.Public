#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Gradeo.CoreApplication.WebAPI/Gradeo.CoreApplication.WebAPI.csproj", "src/Gradeo.CoreApplication.WebAPI/"]
COPY ["src/Gradeo.CoreApplication.Domain/Gradeo.CoreApplication.Domain.csproj", "src/Gradeo.CoreApplication.Domain/"]
COPY ["src/Gradeo.CoreApplication.Infrastructure/Gradeo.CoreApplication.Infrastructure.csproj", "src/Gradeo.CoreApplication.Infrastructure/"]
COPY ["src/Gradeo.CoreApplication.Application/Gradeo.CoreApplication.Application.csproj", "src/Gradeo.CoreApplication.Application/"]
RUN dotnet restore "src/Gradeo.CoreApplication.WebAPI/Gradeo.CoreApplication.WebAPI.csproj"
COPY . .
WORKDIR "/src/src/Gradeo.CoreApplication.WebAPI"
RUN dotnet build "Gradeo.CoreApplication.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Gradeo.CoreApplication.WebAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Gradeo.CoreApplication.WebAPI.dll"]