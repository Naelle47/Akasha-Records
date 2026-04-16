namespace AkashaRecords.Models.ProjectModels;

/// <summary>
/// Représente un élément de Genshin Impact (Anemo, Pyro, etc.).
/// Mappé sur la table <c>elements</c>.
/// </summary>
public class Element
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? IconUrl { get; set; }
}