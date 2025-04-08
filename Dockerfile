FROM mcr.microsoft.com/dotnet/sdk:8.0

WORKDIR /app

COPY Orkaris-Back/*.csproj ./Orkaris-Back/

RUN dotnet restore ./Orkaris-Back/Orkaris-Back.csproj

COPY . .

EXPOSE 5000

CMD ["dotnet", "watch", "--project", "Orkaris-Back", "run", "--urls", "http://0.0.0.0:5000"]
