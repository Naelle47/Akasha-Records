# Akasha Records — Roadmap

> Akasha Records est une refonte complète de GenshinNexus, repensé de zéro avec une architecture propre.
> Ce fichier recense ce qui a été accompli et les améliorations à venir.

---

## ✅ Accompli

### Base de données
- Schéma PostgreSQL complet : `elements`, `weapon_types`, `regions`, `characters`, `weapons`, `artifacts`, `character_roles`
- Schéma de builds flexible : `builds`, `build_weapons`, `build_artifacts`, `build_main_stats`, `build_sub_stats`
- Convention de nommage snake_case cohérente, clés primaires unifiées (`id`)
- 107 personnages, 197 armes, 42 sets d'artefacts

### Architecture
- Models en PascalCase avec mapping Dapper automatique (`MatchNamesWithUnderscores`)
- `IReferenceRepository` regroupant éléments, régions, types d'armes et rôles
- `ICharacterRepository` avec `GetFilteredAsync`, `GetLatestAsync`, `GetByIdAsync`
- `IBuildRepository` avec `GetByCharacterIdAsync` (chargement complet armes + artefacts + stats)
- `ImageHelper` et `CharacterHelper` centralisés

### Vues & UI
- Layout avec sidebar de navigation, thème clair/sombre, modale "À propos"
- `Home/Index` — stats, grille des dernières sorties, placeholder builds
- `Character/Index` — catalogue avec filtres (élément, arme, région, rareté) et compteur
- `Character/Details` — fiche de build complète (armes, artefacts, stats)
- Cartes personnages horizontales avec image 200×200
- Scroll to top, bouton retour vers le catalogue
- Logo et favicon personnalisés

---

## 🏗️ En cours / À venir

### Court terme
- [ ] Transformer la note d'arme (ex : "R5 recommandé") en tag visuel dans la vue Details
- [ ] Vue `Home/Index` — brancher les stats sur la BDD (`GetCountAsync` pour personnages, armes, builds)
- [ ] Vue d'erreur personnalisée (`/Views/Shared/Error.cshtml`) avec gestion 404 / 500
- [ ] Alimenter les builds en base au fur et à mesure

### Moyen terme
- [ ] Gestion centralisée des erreurs — middleware d'exception global (`UseExceptionHandler`)
- [ ] Logger les erreurs (Serilog ou le logger natif ASP.NET Core)
- [ ] Gestion des transactions Dapper (`IDbTransaction`) pour les opérations multi-requêtes
- [ ] Passive abilities des armes (`passive_ability`) — `UPDATE` au fur et à mesure des builds documentés

### Long terme (si auth implémentée)
- [ ] Système d'authentification — ASP.NET Core Identity ou OAuth
  > Prérequis : stabiliser le contenu (builds) avant d'implémenter
- [ ] Marquer un build existant comme "build utilisé" (référence personnelle, pas création from scratch)
- [ ] Personnages favoris / sauvegardés

---

## 📝 Notes techniques

- La convention `IS NULL OR = @Param` dans les requêtes SQL filtrées est intentionnelle — elle permet de gérer tous les cas de filtres avec une seule requête.
- `Snezhnaya` n'a pas encore d'icône en base — fallback vers `Unknown.png` jusqu'à la sortie de la région.
- Les passives d'armes sont `NULL` par défaut — à renseigner via `UPDATE` uniquement pour les armes utilisées dans les builds documentés.
- Le `combo_group` sur `build_artifacts` permet de regrouper deux sets 2pc formant un combo — même numéro = même combinaison.
