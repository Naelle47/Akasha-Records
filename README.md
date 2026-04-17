# Akasha Records

> Référence personnalisée dédiée à Genshin Impact — fiches de builds, catalogue de personnages et d'armes.

Akasha Records est une refonte complète de [GenshinNexus](https://github.com/Naelle47/genshin-nexus), un premier jet fonctionnel mais construit sans conception préalable. Le principe fondamental reste le même — une application de référence centrée sur les builds de personnages — mais l'architecture, le schéma de base de données et les conventions ont été repensés de zéro.

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
- Fiche de build par personnage — armes recommandées, sets d'artefacts, stats principales et sous-stats
- Thème clair / sombre persistant (localStorage)
- Catalogue d'armes *(à venir)*

---

## Ce qui a changé par rapport à GenshinNexus

- **BDD** — schéma entièrement refondu : nommage snake_case cohérent, clés primaires unifiées (`id`), schéma de builds flexible (`build_weapons`, `build_artifacts`, `build_main_stats`, `build_sub_stats`)
- **Models** — PascalCase dès le départ, mapping Dapper automatique via `MatchNamesWithUnderscores`
- **Repositories** — `IReferenceRepository` regroupant les entités simples (éléments, régions, types d'armes, rôles)
- **Helpers** — `ImageHelper` et `CharacterHelper` centralisés dès le départ
- **JS** — `site.js` partagé pour le thème et la modale, JS spécifique par vue uniquement
- **UI** — sidebar de navigation, cartes horizontales, identité visuelle propre

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
