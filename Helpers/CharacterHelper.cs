namespace AkashaRecords.Helpers;

/// <summary>
/// Fournit des méthodes utilitaires pour l'affichage des données de personnages.
/// </summary>
public static class CharacterHelper
{
    /// <summary>
    /// Formate la date de sortie d'un personnage.
    /// Retourne "N/A" si la date est la valeur par défaut.
    /// </summary>
    /// <param name="date">Date de sortie du personnage.</param>
    public static string FormatReleaseDate(DateOnly date)
        => date == default ? "N/A" : date.ToString("dd MMM yyyy");

    /// <summary>
    /// Retourne la rareté du personnage sous forme d'étoiles.
    /// </summary>
    /// <param name="rarity">Rareté du personnage (4 ou 5).</param>
    public static string RarityStars(int rarity)
        => $"{rarity}✦";
}