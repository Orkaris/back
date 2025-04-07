FROM mcr.microsoft.com/dotnet/sdk:8.0
WORKDIR /app
COPY . .

# Restaure les dépendances
RUN dotnet restore "./Orkaris-Back/Orkaris-Back.csproj"

# # Installe l'outil dotnet-ef
# RUN dotnet tool install --global dotnet-ef && \
# 	export PATH="$PATH:/root/.dotnet/tools"

# # Applique les migrations à la base de données
# RUN dotnet ef database update --project "./Orkaris-Back/Orkaris-Back.csproj"

# Expose le port dev
EXPOSE 5000

# Démarre en mode dev avec watch (hot reload)
CMD ["dotnet", "watch", "--project", "Orkaris-Back", "run", "--urls", "http://0.0.0.0:5000"]
