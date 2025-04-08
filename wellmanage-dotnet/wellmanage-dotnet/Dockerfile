FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["wellmanage-dotnet/wellmanage-dotnet.csproj", "wellmanage-dotnet/"]
RUN dotnet restore "wellmanage-dotnet/wellmanage-dotnet.csproj"
COPY . .
WORKDIR "/src/wellmanage-dotnet"
RUN dotnet build "wellmanage-dotnet.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "wellmanage-dotnet.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "wellmanage-dotnet.dll"]
