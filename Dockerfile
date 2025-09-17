FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "TeknikServis.WebAPI/TeknikServis.WebAPI.csproj"
RUN dotnet publish "TeknikServis.WebAPI/TeknikServis.WebAPI.csproj" -c Release -o /app/publish /p:PublishTrimmed=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80
ENTRYPOINT ["dotnet", "TeknikServis.WebAPI.dll"]
