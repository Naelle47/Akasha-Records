namespace AkashaRecords.Models.ProjectModels;

/// <summary>
/// Représente un set d'artefacts recommandé dans un build.
/// Mappé sur la table <c>build_artifacts</c>.
/// PieceCount : 4 pour un set complet, 2 pour une combinaison 2pc+2pc.
/// ComboGroup : regroupe deux lignes 2pc formant un combo (même numéro = même combinaison).
/// </summary>
public class BuildArtifact
{
    public int Id { get; set; }
    public int BuildId { get; set; }
    public int ArtifactId { get; set; }
    public int PieceCount { get; set; }
    public int Priority { get; set; }
    public int? ComboGroup { get; set; }

    // -------------------------
    // Propriété de navigation
    // -------------------------

    public Artifact Artifact { get; set; }
}