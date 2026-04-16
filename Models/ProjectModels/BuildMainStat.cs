namespace AkashaRecords.Models.ProjectModels;

/// <summary>
/// Représente une stat principale recommandée pour un slot d'artefact dans un build.
/// Mappé sur la table <c>build_main_stats</c>.
/// Slot : Sands, Goblet ou Circlet.
/// </summary>
public class BuildMainStat
{
    public int Id { get; set; }
    public int BuildId { get; set; }
    public string Slot { get; set; }
    public string Stat { get; set; }
    public int Priority { get; set; }
}