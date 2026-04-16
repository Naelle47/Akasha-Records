using AkashaRecords.Data.Repositories.CharacterRepo;
using AkashaRecords.Data.Repositories.ReferenceRepo;
using AkashaRecords.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AkashaRecords.Controllers;

/// <summary>
/// Contrôleur responsable de l'affichage et du filtrage des personnages.
/// </summary>
public class CharacterController : Controller
{
    private readonly ICharacterRepository _characterRepo;
    private readonly IReferenceRepository _referenceRepo;

    /// <summary>
    /// Initialise le contrôleur avec les repositories nécessaires via injection de dépendances.
    /// </summary>
    public CharacterController(ICharacterRepository characterRepo, IReferenceRepository referenceRepo)
    {
        _characterRepo = characterRepo;
        _referenceRepo = referenceRepo;
    }

    /// <summary>
    /// Affiche la liste des personnages avec des filtres optionnels.
    /// Tous les paramètres sont optionnels : null signifie "pas de filtre sur ce critère".
    /// </summary>
    /// <param name="elementId">Identifiant de l'élément (Anemo, Pyro, etc.).</param>
    /// <param name="weaponTypeId">Identifiant du type d'arme (Sword, Bow, etc.).</param>
    /// <param name="regionId">Identifiant de la région d'origine.</param>
    /// <param name="rarity">Rareté du personnage (4 ou 5 étoiles).</param>
    public async Task<IActionResult> Index(int? elementId, int? weaponTypeId, int? regionId, int? rarity)
    {
        var characters = await _characterRepo.GetFilteredAsync(elementId, weaponTypeId, regionId, rarity);

        var vm = new CharacterFiltersVM
        {
            ElementId = elementId,
            WeaponTypeId = weaponTypeId,
            RegionId = regionId,
            Rarity = rarity,
            Elements = await _referenceRepo.GetAllElementsAsync(),
            WeaponTypes = await _referenceRepo.GetAllWeaponTypesAsync(),
            Regions = await _referenceRepo.GetAllRegionsAsync(),
            Characters = characters.ToList()
        };

        return View(vm);
    }

    /// <summary>
    /// Affiche la fiche de build d'un personnage spécifique.
    /// </summary>
    /// <param name="id">Identifiant unique du personnage.</param>
    /// <returns>La fiche du personnage, ou NotFound() s'il n'existe pas.</returns>
    /// <remarks>
    /// TODO : Implémenter la récupération du build via IBuildRepository.
    /// </remarks>
    public async Task<IActionResult> Details(int id)
    {
        var character = await _characterRepo.GetByIdAsync(id);

        if (character is null)
            return NotFound();

        var vm = new CharacterDetailsVM
        {
            Character = character
            // Builds : à alimenter via BuildRepo quand implémenté
        };

        return View(vm);
    }
}