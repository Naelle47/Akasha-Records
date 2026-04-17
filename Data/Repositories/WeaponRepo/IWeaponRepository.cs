using AkashaRecords.Models.ProjectModels;

namespace AkashaRecords.Data.Repositories.WeaponRepo;

/// <summary>
/// Contrat définissant les opérations de lecture sur les armes.
/// </summary>
public interface IWeaponRepository
{
    Task<int> GetCountAsync();
    Task<IEnumerable<Weapon>> GetFilteredAsync(int? weaponTypeId, int? rarity);
    Task<Weapon?> GetByIdAsync(int id);
}