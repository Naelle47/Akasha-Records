using AkashaRecords.Models.ProjectModels;
using Dapper;
using System.Data;

namespace AkashaRecords.Data.Repositories.WeaponRepo;

/// <summary>
/// Implémentation de <see cref="IWeaponRepository"/> via Dapper.
/// </summary>
public class WeaponRepository : IWeaponRepository
{
    private readonly IDbConnection _db;

    public WeaponRepository(IDbConnection db)
    {
        _db = db;
    }

    /// <inheritdoc/>
    public async Task<int> GetCountAsync()
    {
        const string sql = "SELECT COUNT(*) FROM weapons;";
        return await _db.ExecuteScalarAsync<int>(sql);
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<Weapon>> GetFilteredAsync(int? weaponTypeId, int? rarity, string? source, string? search, string? letter)
    {
        const string sql = @"
        SELECT
            w.id, w.name, w.rarity, w.weapon_type_id, w.passive_ability, w.icon_url, w.source, w.series,
            wt.id, wt.name, wt.icon_url
        FROM weapons w
        JOIN weapon_types wt ON w.weapon_type_id = wt.id
        WHERE (@WeaponTypeId IS NULL OR w.weapon_type_id = @WeaponTypeId)
          AND (@Rarity       IS NULL OR w.rarity = @Rarity)
          AND (@Source       IS NULL OR w.source = @Source)
          AND (@Search       IS NULL OR LOWER(w.name) LIKE LOWER(@Search))
          AND (@Letter       IS NULL OR UPPER(LEFT(w.name, 1)) = UPPER(@Letter))
        ORDER BY w.rarity DESC, w.name ASC;";

        return await _db.QueryAsync<Weapon, WeaponType, Weapon>(
            sql,
            (weapon, weaponType) =>
            {
                weapon.WeaponType = weaponType;
                return weapon;
            },
            new
            {
                WeaponTypeId = weaponTypeId,
                Rarity = rarity,
                Source = source,
                Search = search != null ? $"%{search}%" : null,
                Letter = letter
            },
            splitOn: "id"
        );
    }

    /// <inheritdoc/>
    public async Task<Weapon?> GetByIdAsync(int id)
    {
        const string sql = @"
            SELECT
                w.id, w.name, w.rarity, w.weapon_type_id, w.passive_ability, w.icon_url,
                wt.id, wt.name, wt.icon_url
            FROM weapons w
            JOIN weapon_types wt ON w.weapon_type_id = wt.id
            WHERE w.id = @Id;";

        return (await _db.QueryAsync<Weapon, WeaponType, Weapon>(
            sql,
            (weapon, weaponType) =>
            {
                weapon.WeaponType = weaponType;
                return weapon;
            },
            new { Id = id },
            splitOn: "id"
        )).FirstOrDefault();
    }
}