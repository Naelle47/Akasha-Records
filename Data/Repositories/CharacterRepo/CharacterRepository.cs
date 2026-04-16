using AkashaRecords.Models.ProjectModels;
using Dapper;
using System.Data;

namespace AkashaRecords.Data.Repositories.CharacterRepo;

/// <summary>
/// Implémentation de <see cref="ICharacterRepository"/> via Dapper.
/// </summary>
public class CharacterRepository : ICharacterRepository
{
    private readonly IDbConnection _db;

    public CharacterRepository(IDbConnection db)
    {
        _db = db;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<Character>> GetFilteredAsync(int? elementId, int? weaponTypeId, int? regionId, int? rarity)
    {
        // IS NULL OR = @Param permet d'ignorer un critère si sa valeur est nulle,
        // évitant de multiplier les requêtes selon les combinaisons de filtres.
        const string sql = @"
            SELECT
                c.id, c.name, c.rarity, c.release_date, c.icon_url,
                c.element_id, c.weapon_type_id, c.region_id,

                e.id, e.name, e.icon_url,
                wt.id, wt.name, wt.icon_url,
                r.id, r.name, r.sort_order, r.icon_url

            FROM characters c
            LEFT JOIN elements     e  ON c.element_id     = e.id
            JOIN weapon_types      wt ON c.weapon_type_id = wt.id
            LEFT JOIN regions      r  ON c.region_id      = r.id
            WHERE (@ElementId    IS NULL OR c.element_id     = @ElementId)
              AND (@WeaponTypeId IS NULL OR c.weapon_type_id = @WeaponTypeId)
              AND (@RegionId     IS NULL OR c.region_id      = @RegionId)
              AND (@Rarity       IS NULL OR c.rarity         = @Rarity)
            ORDER BY c.name;";

        var result = await _db.QueryAsync<Character, Element, WeaponType, Region, Character>(
            sql,
            (c, element, weaponType, region) =>
            {
                c.Element = c.ElementId.HasValue ? element : null;
                c.WeaponType = weaponType;
                c.Region = c.RegionId.HasValue ? region : null;
                return c;
            },
            new { ElementId = elementId, WeaponTypeId = weaponTypeId, RegionId = regionId, Rarity = rarity },
            splitOn: "id,id,id"
        );

        return result;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<Character>> GetLatestAsync(int count)
    {
        const string sql = @"
            SELECT
                c.id, c.name, c.rarity, c.release_date, c.icon_url,
                c.element_id, c.weapon_type_id, c.region_id,

                e.id, e.name, e.icon_url,
                wt.id, wt.name, wt.icon_url,
                r.id, r.name, r.sort_order, r.icon_url

            FROM characters c
            LEFT JOIN elements     e  ON c.element_id     = e.id
            LEFT JOIN weapon_types wt ON c.weapon_type_id = wt.id
            LEFT JOIN regions      r  ON c.region_id      = r.id
            ORDER BY c.release_date DESC
            LIMIT @Count;";

        return await _db.QueryAsync<Character, Element, WeaponType, Region, Character>(
            sql,
            (c, element, weaponType, region) =>
            {
                c.Element = c.ElementId.HasValue ? element : null;
                c.WeaponType = weaponType;
                c.Region = c.RegionId.HasValue ? region : null;
                return c;
            },
            new { Count = count },
            splitOn: "id,id,id"
        );
    }

    /// <inheritdoc/>
    public async Task<Character?> GetByIdAsync(int id)
    {
        const string sql = @"
            SELECT
                c.id, c.name, c.rarity, c.release_date, c.icon_url,
                c.element_id, c.weapon_type_id, c.region_id,

                e.id, e.name, e.icon_url,
                wt.id, wt.name, wt.icon_url,
                r.id, r.name, r.sort_order, r.icon_url

            FROM characters c
            LEFT JOIN elements     e  ON c.element_id     = e.id
            JOIN weapon_types      wt ON c.weapon_type_id = wt.id
            LEFT JOIN regions      r  ON c.region_id      = r.id
            WHERE c.id = @Id;";

        var result = await _db.QueryAsync<Character, Element, WeaponType, Region, Character>(
            sql,
            (c, element, weaponType, region) =>
            {
                c.Element = c.ElementId.HasValue ? element : null;
                c.WeaponType = weaponType;
                c.Region = c.RegionId.HasValue ? region : null;
                return c;
            },
            new { Id = id },
            splitOn: "id,id,id"
        );

        return result.FirstOrDefault();
    }
}