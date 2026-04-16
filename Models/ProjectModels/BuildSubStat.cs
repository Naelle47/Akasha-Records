namespace AkashaRecords.Models.ProjectModels;

/// <summary>
/// Représente une sous-stat prioritaire dans un build.
/// Mappé sur la table <c>build_sub_stats</c>.
/// </summary>
public class BuildSubStat
{
    public int Id { get; set; }
    public int BuildId { get; set; }
    public string Stat { get; set; }
    public int Priority { get; set; }
}