namespace AkashaRecords.Models.ProjectModels;

/// <summary>
/// Représente une arme recommandée dans un build, avec son ordre de priorité.
/// Mappé sur la table <c>build_weapons</c>.
/// </summary>
public class BuildWeapon
{
    public int Id { get; set; }
    public int BuildId { get; set; }
    public int WeaponId { get; set; }
    public int Priority { get; set; }
    public string? Note { get; set; }

    // -------------------------
    // Propriété de navigation
    // -------------------------

    public Weapon Weapon { get; set; }
}