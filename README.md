# Orkaris-Back

Bienvenue dans le projet **Orkaris**. Ce dépôt contient le backend de l'application Orkaris.

## Installation

1. Clonez ce dépôt :

    ```bash
    git clone https://github.com/Orkaris/back.git
    ```

2. Accédez au répertoire du projet :

    ```bash
    cd Orkaris-Back
    ```

## Lancement du serveur

Pour démarrer le serveur en mode développement :

```bash
docker-compose up --build -d
```

## INFO API

La documentation de l'API est disponible via Swagger. Vous pouvez y accéder en lançant le serveur et en visitant le lien suivant :

[http://localhost:5000/swagger/index.html](http://localhost:5000/swagger/index.html)

Pour tester si l'API marche bien faite : 
```bash
curl -X 'GET' \ 'http://localhost:5000/weatherforecast' \ -H 'accept: application/json'
```


Pour la base

```bash
docker exec -it orkaris-back /bin/bash -c "cd Orkaris-Back && ~/.dotnet/tools/dotnet-ef database update"
docker exec -it orkaris-back bash -c 'cd Orkaris-Back && dotnet tool install --global dotnet-ef --version 8.0.0 && export PATH="$PATH:/root/.dotnet/tools" && dotnet ef database update --project Orkaris-Back'

```



