namespace AkashaRecords.Models.ProjectModels;

/// <summary>
/// Représente une région de Teyvat (Mondstadt, Liyue, etc.).
/// Mappé sur la table <c>regions</c>.
/// </summary>
public class Region
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int SortOrder { get; set; }
    public string? IconUrl { get; set; }
}