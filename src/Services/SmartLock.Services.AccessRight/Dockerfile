FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY src/Services/SmartLock.Services.AccessRight/SmartLock.Services.AccessRight.csproj src/Services/SmartLock.Services.AccessRight/
RUN dotnet restore src/Services/SmartLock.Services.AccessRight/SmartLock.Services.AccessRight.csproj
COPY . .
WORKDIR /src/src/Services/SmartLock.Services.AccessRight
RUN dotnet build SmartLock.Services.AccessRight.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish SmartLock.Services.AccessRight.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "SmartLock.Services.AccessRight.dll"]
