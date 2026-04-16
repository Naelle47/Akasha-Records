namespace AkashaRecords.Models.ProjectModels;

/// <summary>
/// Représente le rôle d'un personnage dans un build (Main DPS, Support, etc.).
/// Mappé sur la table <c>character_roles</c>.
/// </summary>
public class CharacterRole
{
    public int Id { get; set; }
    public string Name { get; set; }
}