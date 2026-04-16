# Akasha Records — Roadmap

> Ce fichier recense les améliorations techniques, corrections et fonctionnalités à intégrer.
> Il sert de référence pour reprendre le projet après une interruption.

---

## 🔄 Reconstruction du projet

> Le projet sera reconstruit à neuf en suivant cet ordre de priorité.
> Le code existant sert de référence — les erreurs identifiées sont documentées ci-dessous.

1. **BDD** — refondre le schéma en partant des tables stables (`characters`, `elements`, `weapon_types`, `regions`, `weapons`, `artifacts`, `characters_roles`) et en intégrant directement `builds`, `build_weapons`, `build_artifacts`, `build_stats`
2. **Models** — PascalCase dès le départ, mapping Dapper configuré proprement
3. **Repositories** — `IReferenceRepository` d'emblée, `ICharacterRepository` avec `GetByIdAsync`
4. **Controllers & Views** — une fois la base de données et les models stabilisés
5. **Helpers** — intégrés dès le début dans `Helpers/CharacterHelper.cs`
6. **JS** — organisé dès le départ (`site.js` pour le partagé, fichiers séparés pour le spécifique)

---

## 🔧 Dette technique (à corriger en priorité)

### Conventions de nommage
- [ ] Renommer les propriétés des modèles de `snake_case` vers `PascalCase`
  (`characterid_pk` → `CharacterId`, `icon_url` → `IconUrl`, etc.)
- [ ] Configurer le mapping Dapper en conséquence (`DefaultTypeMap` ou annotations)

### JavaScript
- [ ] Déplacer le script de **gestion du thème clair/sombre** dans `wwwroot/js/site.js`
- [ ] Déplacer le script de **gestion de la modale "À propos"** dans `wwwroot/js/site.js`
- [ ] Ne conserver dans les `@section Scripts` des vues que le JS spécifique à la page concernée (ex : slider)

### `using` inutilisés
- [ ] Supprimer les `using` superflus identifiés dans `CharacterRepository.cs`
  (`System.Collections`, `System.Reflection.Metadata.Ecma335`)

---

## 🏗️ Architecture & structure

### Repositories
- [ ] Regrouper `ElementRepo`, `RegionRepo` et `WeaponTypeRepo` dans un unique `IReferenceRepository`
  Ces trois repositories n'exposent qu'un `GetAll` simple — les maintenir séparément est inutilement verbeux


- [ ] Implémenter un middleware d'exception global (`UseExceptionHandler`)
- [ ] Créer une vue d'erreur personnalisée (`/Views/Shared/Error.cshtml`)
- [ ] Envisager un filtre d'exception (`IExceptionFilter`) pour les cas spécifiques au niveau des controllers
- [ ] Logger les erreurs (Serilog ou le logger natif ASP.NET Core)

### Gestion des transactions (Dapper)
- [ ] Encapsuler les opérations multi-requêtes dans des transactions via `IDbTransaction`
- [ ] Envisager un pattern **Unit of Work** si le nombre de repositories augmente
  ```
  Exemple minimal avec Dapper :
  using var transaction = _db.BeginTransaction();
  // ... opérations ...
  transaction.Commit();
  ```

---

## 📄 Fonctionnalités à implémenter

### Court terme
- [ ] `CharacterController.Details` : implémenter la récupération via `GetByIdAsync`
  - Ajouter `GetByIdAsync(int id)` dans `ICharacterRepository` et `CharacterRepository`
  - Gérer le cas `NotFound()` si le personnage n'existe pas
- [ ] Revoir la page d'accueil (`Home/Index`) : remplacer le slider par une autre mise en page
  (carousel CSS natif, grille statique, section "à la une", etc. — à définir)

### Moyen terme — Fiche de build personnage
> Angle retenu : **référence personnalisée** (pas un wiki exhaustif)
> Référence visuelle : genshin.gg/builds

Chaque fiche personnage affichera :
- Son rôle (Main DPS, Sub DPS, Support...)
- Les meilleures armes recommandées (ordonnées par priorité)
- Les meilleurs sets d'artefacts (4pc ou combinaisons 2pc+2pc)
- Les stats principales à viser (Sands, Goblet, Circlet)
- Les sous-stats prioritaires

#### État actuel de la base de données
> Le schéma a été commencé sans conception préalable — une refonte partielle est nécessaire.

Tables déjà en place (✅ à conserver, ⚠️ à revoir) :
- ✅ `characters`, `elements`, `weapon_types`, `regions`
- ✅ `weapons` (avec `passive_ability` — bon ajout)
- ✅ `artifacts` (avec `bonus_2pc` et `bonus_4pc`)
- ✅ `characters_roles`
- ⚠️ `char_build` — trop simpliste : une seule `artifactid_fk` et une seule `weaponid_fk`
  Problèmes identifiés :
  - Impossible d'afficher plusieurs armes recommandées avec priorité
  - Impossible de gérer les combos 2pc+2pc d'artefacts
  - Pas de gestion des stats recommandées (Sands / Goblet / Circlet + sous-stats)

#### Refonte du schéma à prévoir
- [ ] Remplacer `char_build` par un schéma plus flexible :
  - `builds` — table centrale (characterid_fk, roleid_fk, build_name)
  - `build_weapons` — liaison Build ↔ Weapon avec champ `priority`
  - `build_artifacts` — liaison Build ↔ Artifact avec champ `priority` et `piece_count` (2pc ou 4pc)
  - `build_stats` — stats recommandées (slot : Sands/Goblet/Circlet, main_stat, sous-stats)

#### Modèles C# à créer (après refonte BDD)
- [ ] `Weapon.cs`
- [ ] `Build.cs`
- [ ] `BuildWeapon.cs` (avec propriété `Priority`)
- [ ] `BuildArtifact.cs` (avec `Priority` et `PieceCount`)
- [ ] `BuildStat.cs`

#### Repositories à créer (après refonte BDD)
- [ ] `IWeaponRepository` / `WeaponRepository`
- [ ] `IBuildRepository` / `BuildRepository` (requête principale de la fiche)

#### Vue à créer
- [ ] `Character/Details.cshtml` — fiche de build complète

### Long terme (si auth implémentée)
- [ ] Système d'authentification (reporté volontairement)
  - Option envisagée : ASP.NET Core Identity ou solution OAuth (Google, etc.)
  - Prérequis avant d'implémenter : stabiliser la base de données et les modèles
- [ ] Builds personnalisés par utilisateur
- [ ] Tierlist personnalisée
- [ ] Suivi de progression (personnages débloqués, constellation, etc.)

---

## 📁 Structure cible des fichiers JS

```
wwwroot/
└── js/
    ├── site.js       ← thème + modale (partagés sur toutes les pages)
    └── slider.js     ← slider Home/Index (spécifique à cette vue)
```

---

## 🛠️ Helpers à créer

> Emplacement cible : `Helpers/CharacterHelper.cs` (classe statique)
> Règle : tout formatage ou logique répété dans deux vues ou plus → candidat Helper.

- [ ] `IconUrl(string path)` — encapsule le `Url.Content("~" + path)` répété sur toutes les entités
- [ ] `RegionIconUrl(Region region)` — encapsule le fallback vers `Emblem_Nation_Unknown_White.png` si `icon_url` est vide
- [ ] `FormatReleaseDate(DateOnly date)` — encapsule `date == default ? "N/A" : date.ToString("dd MMM yyyy")`
- [ ] `RarityStars(int rarity)` — basique pour l'instant (`@rarity★`), à enrichir si affichage stylisé souhaité (icônes, couleurs 4★/5★)

---

## 📝 Notes diverses

- La convention `IS NULL OR = @Param` dans les requêtes SQL filtrées est intentionnelle :
  elle permet de gérer tous les cas de filtres avec une seule requête.
- Le fallback vers `Emblem_Nation_Unknown_White.png` pour les régions sans icône est temporaire :
  à remplacer quand toutes les régions auront leur icône définie en base.
- `GetLatestAsync(int count)` : le `count` est actuellement codé en dur à 5 dans le controller.
  Envisager une constante ou une configuration.
