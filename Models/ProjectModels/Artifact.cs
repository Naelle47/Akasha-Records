namespace AkashaRecords.Models.ProjectModels;

/// <summary>
/// Représente un set d'artefacts de Genshin Impact.
/// Mappé sur la table <c>artifacts</c>.
/// </summary>
public class Artifact
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Bonus2Pc { get; set; }
    public string? Bonus4Pc { get; set; }
    public string? IconUrl { get; set; }
}