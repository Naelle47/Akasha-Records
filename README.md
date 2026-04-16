# Akasha Records

> Référence personnalisée dédiée à Genshin Impact — fiches de builds, catalogue de personnages et d'armes.

---

## Aperçu

<!-- Screenshots à ajouter -->

---

## Stack technique

| Couche | Technologie |
|---|---|
| Framework | ASP.NET Core MVC (.NET 10) |
| Base de données | PostgreSQL |
| Accès données | Dapper 2.1.66 |
| Driver PostgreSQL | Npgsql 10.0.0 |
| Frontend | Razor Views, CSS custom |

---

## Fonctionnalités

- Page d'accueil avec stats et dernières sorties de personnages
- Catalogue de personnages avec filtres (élément, type d'arme, région, rareté)
- Compteur de résultats dynamique selon les filtres appliqués
- Thème clair / sombre persistant (localStorage)
- Fiche de build par personnage *(en cours)*
- Catalogue d'armes *(à venir)*

---

## Prérequis

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [PostgreSQL](https://www.postgresql.org/download/)

---

## Installation

### 1. Cloner le dépôt

```bash
git clone https://github.com/Naelle47/akasha-records.git
cd akasha-records
```

### 2. Configurer la base de données

Créer une base PostgreSQL et exécuter les scripts SQL disponibles dans la documentation Notion du projet.

> ⚠️ Le fichier `appsettings.json` n'est pas inclus dans le dépôt (données sensibles).
> Créer le fichier à la racine du projet avec le contenu suivant :

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=AkashaRecords;Username=xxx;Password=xxx"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

### 3. Lancer le projet

```bash
dotnet run
```

L'application est accessible sur `https://localhost:5001`.

---

## Structure du projet

```
AkashaRecords/
├── Controllers/                  # Logique de routage et d'orchestration
├── Data/
│   └── Repositories/             # Accès base de données (Dapper)
│       ├── CharacterRepo/
│       ├── ReferenceRepo/        # Éléments, régions, types d'armes, rôles
│       ├── WeaponRepo/
│       └── BuildRepo/
├── Helpers/                      # Utilitaires (ImageHelper, CharacterHelper)
├── Models/
│   ├── ProjectModels/            # Entités métier (Character, Weapon, Build, etc.)
│   └── ViewModels/               # Modèles dédiés aux vues
├── Views/
│   ├── Character/
│   ├── Home/
│   └── Shared/                   # Layout, ViewImports, ViewStart
└── wwwroot/
    ├── css/                      # Feuilles de style
    ├── images/                   # Assets visuels
    │   ├── characters/
    │   ├── elements/
    │   ├── regions/
    │   ├── weapon_types/
    │   ├── weapons/
    │   └── artifacts/
    └── js/                       # Scripts
```

---

## Roadmap

Voir [ROADMAP.md](./ROADMAP.md) pour le détail des améliorations prévues.

---

## Ressources

- [Genshin Impact](https://genshin.hoyoverse.com)
- *Ce projet est un projet personnel sans affiliation avec HoYoverse.*
