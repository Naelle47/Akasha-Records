using AkashaRecords.Models.ProjectModels;

namespace AkashaRecords.Data.Repositories.CharacterRepo;

/// <summary>
/// Contrat définissant les opérations de lecture sur les personnages.
/// </summary>
public interface ICharacterRepository
{
    Task<IEnumerable<Character>> GetFilteredAsync(int? elementId, int? weaponTypeId, int? regionId, int? rarity);
    Task<IEnumerable<Character>> GetLatestAsync(int count);
    Task<Character?> GetByIdAsync(int id);
    Task<int> GetCountAsync();
}