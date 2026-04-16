using AkashaRecords.Models.ProjectModels;

namespace AkashaRecords.Models.ViewModels;

/// <summary>
/// ViewModel de la vue Index des personnages.
/// Regroupe les filtres actifs et les listes nécessaires au rendu de la page.
/// </summary>
public class CharacterFiltersVM
{
    // -------------------------
    // Filtres actifs (valeurs sélectionnées par l'utilisateur)
    // Nullable : null signifie "aucun filtre appliqué sur ce critère"
    // -------------------------

    public int? ElementId { get; set; }
    public int? WeaponTypeId { get; set; }
    public int? RegionId { get; set; }
    public int? Rarity { get; set; }

    // -------------------------
    // Données pour alimenter les listes déroulantes
    // Initialisées à vide pour éviter les NullReferenceException dans les vues
    // -------------------------

    public IEnumerable<Element> Elements { get; set; } = Enumerable.Empty<Element>();
    public IEnumerable<WeaponType> WeaponTypes { get; set; } = Enumerable.Empty<WeaponType>();
    public IEnumerable<Region> Regions { get; set; } = Enumerable.Empty<Region>();
    public IReadOnlyList<Character> Characters { get; set; } = Array.Empty<Character>();
}