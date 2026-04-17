namespace AkashaRecords.Models.ProjectModels;

/// <summary>
/// Représente un build pour un personnage donné.
/// Mappé sur la table <c>builds</c>.
/// Un même personnage peut avoir plusieurs builds (ex : Hu Tao Main DPS, Hu Tao Sub DPS).
/// </summary>
public class Build
{
    public int Id { get; set; }
    public int CharacterId { get; set; }
    public int RoleId { get; set; }
    public string Name { get; set; }
    public string? StatThresholds { get; set; }
    public string? StatGoals { get; set; }

    // -------------------------
    // Propriétés de navigation
    // -------------------------

    public Character Character { get; set; }
    public CharacterRole Role { get; set; }
    public IEnumerable<BuildWeapon> Weapons { get; set; } = Enumerable.Empty<BuildWeapon>();
    public IEnumerable<BuildArtifact> Artifacts { get; set; } = Enumerable.Empty<BuildArtifact>();
    public IEnumerable<BuildMainStat> MainStats { get; set; } = Enumerable.Empty<BuildMainStat>();
    public IEnumerable<BuildSubStat> SubStats { get; set; } = Enumerable.Empty<BuildSubStat>();
}