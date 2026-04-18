# Akasha Records — Roadmap

> Akasha Records est une refonte complète de GenshinNexus, repensé de zéro avec une architecture propre.
> Ce fichier recense ce qui a été accompli et les améliorations à venir.

---

## ✅ Accompli

### Base de données
- Schéma PostgreSQL complet : `elements`, `weapon_types`, `regions`, `characters`, `weapons`, `artifacts`, `character_roles`
- Schéma de builds flexible : `builds`, `build_weapons`, `build_artifacts`, `build_main_stats`, `build_sub_stats`
- Convention de nommage snake_case cohérente, clés primaires unifiées (`id`)
- 112 personnages, 197 armes, 42 sets d'artefacts
- Passives des 197 armes renseignées via script SQL

### Architecture
- Models en PascalCase avec mapping Dapper automatique (`MatchNamesWithUnderscores`)
- `IReferenceRepository` regroupant éléments, régions, types d'armes et rôles
- `ICharacterRepository` avec `GetFilteredAsync`, `GetLatestAsync`, `GetByIdAsync`, `GetCountAsync`
- `IBuildRepository` avec `GetByCharacterIdAsync`, `GetLatestAsync`, `GetCountAsync`
- `IWeaponRepository` avec `GetFilteredAsync`, `GetByIdAsync`, `GetCountAsync`
- `ImageHelper` et `CharacterHelper` centralisés

### Vues & UI
- Layout avec sidebar de navigation, thème clair/sombre, modale "À propos"
- `Home/Index` — stats BDD (personnages, armes, builds), grille des dernières sorties, derniers builds
- `Character/Index` — catalogue avec filtres (élément, arme, région, rareté) et compteur
- `Character/Details` — fiche de build complète (armes, artefacts, stats principales, sous-stats)
- Stats à viser et seuils CRIT sur chaque build
- Tag visuel sur les notes d'armes (ex : R5)
- Tooltips armes (passive) et artefacts (bonus 2pc/4pc) au survol
- Disclaimer sur chaque build card
- Cartes personnages horizontales avec image 200×200
- Scroll to top, bouton retour vers le catalogue
- Logo et favicon personnalisés

### Builds documentés
- Albedo — Sub DPS Standard
- Varka — Main DPS Standard

---

## 🏗️ En cours / À venir

### Court terme
- [ ] Catalogue des armes — `WeaponController` + vue Index (filtres type + rareté) et vue Details
- [ ] Vue d'erreur personnalisée (`/Views/Shared/Error.cshtml`) avec gestion 404 / 500
- [ ] Alimenter les builds en base au fur et à mesure

### Moyen terme
- [ ] Gestion centralisée des erreurs — middleware d'exception global (`UseExceptionHandler`)
- [ ] Logger les erreurs (Serilog ou le logger natif ASP.NET Core)
- [ ] Gestion des transactions Dapper (`IDbTransaction`) pour les opérations multi-requêtes

### Long terme (post-CDA)
- [ ] Déploiement — Railway ou Azure App Service
- [ ] Captures d'écran + mise à jour README
- [ ] Icône Snezhnaya — quand la région sera disponible en jeu
- [ ] Système d'authentification — ASP.NET Core Identity ou OAuth
  > Prérequis : stabiliser le contenu (builds) avant d'implémenter
- [ ] Marquer un build existant comme "build utilisé" (référence personnelle, pas création from scratch)
- [ ] Personnages favoris / sauvegardés

---

## 📝 Notes techniques

- La convention `IS NULL OR = @Param` dans les requêtes SQL filtrées est intentionnelle — elle permet de gérer tous les cas de filtres avec une seule requête.
- Snezhnaya n'a pas encore d'icône en base — fallback vers `Unknown.png` jusqu'à la sortie de la région.
- Les passives d'armes sont renseignées pour les 197 armes — à maintenir lors de l'ajout de nouvelles armes.
- Le `combo_group` sur `build_artifacts` permet de regrouper deux sets 2pc formant un combo — même numéro = même combinaison.
- `stat_thresholds` et `stat_goals` sur `builds` — valeurs de référence issues d'un mix entre sources communautaires (Sephijin, Akenouille, genshin.gg).
