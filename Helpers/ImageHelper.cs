namespace AkashaRecords.Helpers;

/// <summary>
/// Fournit des méthodes utilitaires pour la construction des URLs d'images.
/// Le nom de fichier est stocké en base de données, le chemin est géré ici.
/// </summary>
public static class ImageHelper
{
    /// <summary>Retourne l'URL complète de l'icône d'un personnage.</summary>
    /// <param name="fileName">Nom du fichier (ex : Albedo_Icon.png).</param>
    public static string CharacterIcon(string? fileName)
        => string.IsNullOrEmpty(fileName) ? "/images/characters/default.png" : $"/images/characters/{fileName}";

    /// <summary>Retourne l'URL complète de l'icône d'un élément.</summary>
    /// <param name="fileName">Nom du fichier (ex : Element_Pyro.png).</param>
    public static string ElementIcon(string? fileName)
        => string.IsNullOrEmpty(fileName) ? "/images/elements/default.png" : $"/images/elements/{fileName}";

    /// <summary>Retourne l'URL complète de l'icône d'un type d'arme.</summary>
    /// <param name="fileName">Nom du fichier (ex : Icon_Sword.png).</param>
    public static string WeaponTypeIcon(string? fileName)
        => string.IsNullOrEmpty(fileName) ? "/images/weapon_types/default.png" : $"/images/weapon_types/{fileName}";

    /// <summary>
    /// Retourne l'URL complète de l'icône d'une région.
    /// Fallback vers une icône générique si l'icône n'est pas encore définie (ex : Snezhnaya).
    /// </summary>
    /// <param name="fileName">Nom du fichier (ex : Mondstadt.png).</param>
    public static string RegionIcon(string? fileName)
        => string.IsNullOrEmpty(fileName) ? "/images/regions/Unknown.png" : $"/images/regions/{fileName}";

    /// <summary>Retourne l'URL complète de l'icône d'une arme.</summary>
    /// <param name="fileName">Nom du fichier.</param>
    public static string WeaponIcon(string? fileName)
        => string.IsNullOrEmpty(fileName) ? "/images/weapons/default.png" : $"/images/weapons/{fileName}";

    /// <summary>Retourne l'URL complète de l'icône d'un set d'artefacts.</summary>
    /// <param name="fileName">Nom du fichier.</param>
    public static string ArtifactIcon(string? fileName)
        => string.IsNullOrEmpty(fileName) ? "/images/artifacts/default.png" : $"/images/artifacts/{fileName}";
}