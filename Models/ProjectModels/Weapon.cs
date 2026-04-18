namespace AkashaRecords.Models.ProjectModels;

/// <summary>
/// Représente une arme de Genshin Impact.
/// Mappé sur la table <c>weapons</c>.
/// </summary>
public class Weapon
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Rarity { get; set; }
    public int WeaponTypeId { get; set; }
    public string? PassiveAbility { get; set; }
    public string? IconUrl { get; set; }
    public string? Source { get; set; }

    // -------------------------
    // Propriété de navigation
    // -------------------------

    public WeaponType WeaponType { get; set; }
}