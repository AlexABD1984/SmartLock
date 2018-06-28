FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY src/Services/SmartLock.Services.Locking.API/SmartLock.Services.Locking.API.csproj src/Services/SmartLock.Services.Locking.API/
RUN dotnet restore src/Services/SmartLock.Services.Locking.API/SmartLock.Services.Locking.API.csproj
COPY . .
WORKDIR /src/src/Services/SmartLock.Services.Locking.API
RUN dotnet build SmartLock.Services.Locking.API.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish SmartLock.Services.Locking.API.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "SmartLock.Services.Locking.API.dll"]
