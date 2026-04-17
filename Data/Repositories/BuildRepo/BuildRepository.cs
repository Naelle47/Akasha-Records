using AkashaRecords.Models.ProjectModels;
using Dapper;
using System.Data;

namespace AkashaRecords.Data.Repositories.BuildRepo;

/// <summary>
/// Implémentation de <see cref="IBuildRepository"/> via Dapper.
/// </summary>
public class BuildRepository : IBuildRepository
{
    private readonly IDbConnection _db;

    public BuildRepository(IDbConnection db)
    {
        _db = db;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<Build>> GetByCharacterIdAsync(int characterId)
    {
        // Étape 1 : récupérer les builds avec leur rôle
        const string sqlBuilds = @"
            SELECT
                b.id, b.character_id, b.role_id, b.name,
                cr.id, cr.name
            FROM builds b
            JOIN character_roles cr ON b.role_id = cr.id
            WHERE b.character_id = @CharacterId
            ORDER BY b.id;";

        var builds = (await _db.QueryAsync<Build, CharacterRole, Build>(
            sqlBuilds,
            (build, role) =>
            {
                build.Role = role;
                return build;
            },
            new { CharacterId = characterId },
            splitOn: "id"
        )).ToList();

        if (!builds.Any())
            return builds;

        var buildIds = builds.Select(b => b.Id).ToList();

        // Étape 2 : récupérer les armes recommandées
        const string sqlWeapons = @"
            SELECT
                bw.id, bw.build_id, bw.weapon_id, bw.priority, bw.note,
                w.id, w.name, w.rarity, w.passive_ability, w.icon_url
            FROM build_weapons bw
            JOIN weapons w ON bw.weapon_id = w.id
            WHERE bw.build_id = ANY(@BuildIds)
            ORDER BY bw.build_id, bw.priority;";

        var buildWeapons = await _db.QueryAsync<BuildWeapon, Weapon, BuildWeapon>(
            sqlWeapons,
            (bw, weapon) =>
            {
                bw.Weapon = weapon;
                return bw;
            },
            new { BuildIds = buildIds.ToArray() },
            splitOn: "id"
        );

        // Étape 3 : récupérer les artefacts recommandés
        const string sqlArtifacts = @"
            SELECT
                ba.id, ba.build_id, ba.artifact_id, ba.piece_count, ba.priority, ba.combo_group,
                a.id, a.name, a.bonus_2pc, a.bonus_4pc, a.icon_url
            FROM build_artifacts ba
            JOIN artifacts a ON ba.artifact_id = a.id
            WHERE ba.build_id = ANY(@BuildIds)
            ORDER BY ba.build_id, ba.priority, ba.combo_group;";

        var buildArtifacts = await _db.QueryAsync<BuildArtifact, Artifact, BuildArtifact>(
            sqlArtifacts,
            (ba, artifact) =>
            {
                ba.Artifact = artifact;
                return ba;
            },
            new { BuildIds = buildIds.ToArray() },
            splitOn: "id"
        );

        // Étape 4 : récupérer les stats principales
        const string sqlMainStats = @"
            SELECT id, build_id, slot, stat, priority
            FROM build_main_stats
            WHERE build_id = ANY(@BuildIds)
            ORDER BY build_id, slot, priority;";

        var mainStats = await _db.QueryAsync<BuildMainStat>(
            sqlMainStats,
            new { BuildIds = buildIds.ToArray() }
        );

        // Étape 5 : récupérer les sous-stats
        const string sqlSubStats = @"
            SELECT id, build_id, stat, priority
            FROM build_sub_stats
            WHERE build_id = ANY(@BuildIds)
            ORDER BY build_id, priority;";

        var subStats = await _db.QueryAsync<BuildSubStat>(
            sqlSubStats,
            new { BuildIds = buildIds.ToArray() }
        );

        // Assemblage : on rattache chaque collection à son build
        foreach (var build in builds)
        {
            build.Weapons = buildWeapons.Where(w => w.BuildId == build.Id).ToList();
            build.Artifacts = buildArtifacts.Where(a => a.BuildId == build.Id).ToList();
            build.MainStats = mainStats.Where(s => s.BuildId == build.Id).ToList();
            build.SubStats = subStats.Where(s => s.BuildId == build.Id).ToList();
        }

        return builds;
    }
}