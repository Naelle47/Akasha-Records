using AkashaRecords.Models.ProjectModels;

namespace AkashaRecords.Data.Repositories.WeaponRepo;

/// <summary>
/// Contrat définissant les opérations de lecture sur les armes.
/// </summary>
public interface IWeaponRepository
{
    Task<int> GetCountAsync();
    Task<IEnumerable<Weapon>> GetFilteredAsync(int? weaponTypeId, int? rarity, string? source, string? search, string? letter);
    Task<Weapon?> GetByIdAsync(int id);
}