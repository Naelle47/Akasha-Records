using AkashaRecords.Models.ProjectModels;
using Dapper;
using System.Data;

namespace AkashaRecords.Data.Repositories.ReferenceRepo;

/// <summary>
/// Implémentation de <see cref="IReferenceRepository"/> via Dapper.
/// </summary>
public class ReferenceRepository : IReferenceRepository
{
    private readonly IDbConnection _db;

    public ReferenceRepository(IDbConnection db)
    {
        _db = db;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<Element>> GetAllElementsAsync()
    {
        const string sql = "SELECT id, name, icon_url FROM elements ORDER BY name;";
        return await _db.QueryAsync<Element>(sql);
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<WeaponType>> GetAllWeaponTypesAsync()
    {
        const string sql = "SELECT id, name, icon_url FROM weapon_types ORDER BY name;";
        return await _db.QueryAsync<WeaponType>(sql);
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<Region>> GetAllRegionsAsync()
    {
        const string sql = "SELECT id, name, sort_order, icon_url FROM regions ORDER BY sort_order;";
        return await _db.QueryAsync<Region>(sql);
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<CharacterRole>> GetAllCharacterRolesAsync()
    {
        const string sql = "SELECT id, name FROM character_roles ORDER BY name;";
        return await _db.QueryAsync<CharacterRole>(sql);
    }
}