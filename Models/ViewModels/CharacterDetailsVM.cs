using AkashaRecords.Models.ProjectModels;

namespace AkashaRecords.Models.ViewModels;

/// <summary>
/// ViewModel de la vue Details d'un personnage.
/// Regroupe les infos du personnage et ses builds associés.
/// </summary>
public class CharacterDetailsVM
{
    public Character Character { get; set; }

    /// <summary>
    /// Builds du personnage — vide tant que BuildRepo n'est pas implémenté.
    /// </summary>
    public IReadOnlyList<Build> Builds { get; set; } = Array.Empty<Build>();
}