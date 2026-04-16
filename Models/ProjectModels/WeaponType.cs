namespace AkashaRecords.Models.ProjectModels;

/// <summary>
/// Représente un type d'arme (Sword, Bow, etc.).
/// Mappé sur la table <c>weapon_types</c>.
/// </summary>
public class WeaponType
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? IconUrl { get; set; }
}