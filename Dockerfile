FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 8000

ENV ASPNETCORE_URLS=http://+:8000;http://+:80;
ENV ASPNETCORE_ENVIRONMENT=Development

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Ticket.csproj", "."]
RUN dotnet restore "./Ticket.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "Ticket.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ticket.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ticket.dll"]