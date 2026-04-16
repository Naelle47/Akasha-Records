using AkashaRecords.Models.ProjectModels;

namespace AkashaRecords.Data.Repositories.ReferenceRepo;

/// <summary>
/// Contrat définissant les opérations de lecture sur les tables de référence
/// (éléments, types d'armes, régions, rôles).
/// </summary>
public interface IReferenceRepository
{
    Task<IEnumerable<Element>> GetAllElementsAsync();
    Task<IEnumerable<WeaponType>> GetAllWeaponTypesAsync();
    Task<IEnumerable<Region>> GetAllRegionsAsync();
    Task<IEnumerable<CharacterRole>> GetAllCharacterRolesAsync();
}