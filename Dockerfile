# Utiliser l'image officielle de .NET SDK pour construire l'application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Définir le répertoire de travail dans le conteneur
WORKDIR /src

# Copier les fichiers de votre projet dans le conteneur
COPY ["gestioncommande.csproj", "./"]

# Restaurer les dépendances (cela va télécharger les packages NuGet)
RUN dotnet restore "gestioncommande.csproj"

# Copier tous les fichiers du projet dans le conteneur
COPY . .

# Publier l'application en mode release
RUN dotnet publish "gestioncommande.csproj" -c Release -o /app/publish

# Utiliser la même image pour l'exécution
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

# Définir le répertoire de travail pour l'exécution
WORKDIR /app

# Copier l'application publiée depuis l'image précédente
COPY --from=build /app/publish .

# Exposer le port que l'application écoute
EXPOSE 80

# Démarrer l'application
ENTRYPOINT ["dotnet", "gestioncommande.dll"]