FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
COPY . .

ARG CONFIGURATION=Release
RUN dotnet restore
RUN dotnet build -c $CONFIGURATION --no-restore
RUN dotnet publish -c $CONFIGURATION --no-build --no-restore -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./
EXPOSE 80
EXPOSE 443

ENTRYPOINT ["dotnet", "RichillCapital.Exchange.Api.dll"]