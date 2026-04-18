using AkashaRecords.Models.ProjectModels;

namespace AkashaRecords.Models.ViewModels;

/// <summary>
/// ViewModel de la vue Index des armes.
/// Regroupe les armes filtrées et les données de référence pour les filtres.
/// </summary>
public class WeaponFiltersVM
{
    public IReadOnlyList<Weapon> Weapons { get; set; } = Array.Empty<Weapon>();
    public IReadOnlyList<WeaponType> WeaponTypes { get; set; } = Array.Empty<WeaponType>();

    public int? WeaponTypeId { get; set; }
    public int? Rarity { get; set; }
    public string? Source { get; set; }
}