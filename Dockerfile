FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY . ./
RUN dotnet restore "./Orkaris-Back/Orkaris-Back.csproj"

RUN dotnet tool install --global dotnet-ef --version 8.0.0 # Spécifiez la version des outils EF Core

EXPOSE 5000

CMD ["dotnet", "watch", "--project", "Orkaris-Back", "run", "--urls", "http://0.0.0.0:5000"]
