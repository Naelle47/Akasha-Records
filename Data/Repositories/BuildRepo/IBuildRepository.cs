using AkashaRecords.Models.ProjectModels;

namespace AkashaRecords.Data.Repositories.BuildRepo;

/// <summary>
/// Contrat définissant les opérations de lecture sur les builds de personnages.
/// </summary>
public interface IBuildRepository
{
    /// <summary>
    /// Récupère tous les builds d'un personnage, avec armes, artefacts et stats associés.
    /// </summary>
    /// <param name="characterId">Identifiant unique du personnage.</param>
    Task<IEnumerable<Build>> GetByCharacterIdAsync(int characterId);
    Task<int> GetCountAsync();
    Task<IEnumerable<Build>> GetLatestAsync(int count);
}