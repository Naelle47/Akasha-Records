namespace AkashaRecords.Models.ProjectModels;

/// <summary>
/// Représente un personnage jouable de Genshin Impact.
/// Mappé sur la table <c>characters</c>.
/// </summary>
public class Character
{
    // -------------------------
    // Colonnes de la table characters
    // -------------------------

    public int Id { get; set; }
    public string Name { get; set; }
    public int Rarity { get; set; }
    public DateOnly ReleaseDate { get; set; }
    public string? IconUrl { get; set; }

    /// <summary>FK nullable — null si le personnage n'a pas d'élément fixe (Traveler, Skirk...).</summary>
    public int? ElementId { get; set; }

    public int WeaponTypeId { get; set; }

    /// <summary>FK nullable — null si le personnage n'est rattaché à aucune région (Aloy, Traveler...).</summary>
    public int? RegionId { get; set; }

    // -------------------------
    // Propriétés de navigation (Many-to-One)
    // -------------------------

    public Element? Element { get; set; }
    public WeaponType WeaponType { get; set; }
    public Region? Region { get; set; }
}