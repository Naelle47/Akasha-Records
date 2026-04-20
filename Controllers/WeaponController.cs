using AkashaRecords.Data.Repositories.ReferenceRepo;
using AkashaRecords.Data.Repositories.WeaponRepo;
using AkashaRecords.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AkashaRecords.Controllers;

/// <summary>
/// Contrôleur du catalogue des armes.
/// </summary>
public class WeaponController : Controller
{
    private readonly IWeaponRepository _weaponRepo;
    private readonly IReferenceRepository _referenceRepo;

    public WeaponController(
        IWeaponRepository weaponRepo,
        IReferenceRepository referenceRepo)
    {
        _weaponRepo = weaponRepo;
        _referenceRepo = referenceRepo;
    }

    /// <summary>
    /// Catalogue des armes avec filtres par type, rareté, source, recherche et lettre.
    /// </summary>
    public async Task<IActionResult> Index(int? weaponTypeId, int? rarity, string? source, string? search, string? letter)
    {
        var vm = new WeaponFiltersVM
        {
            Weapons = (await _weaponRepo.GetFilteredAsync(weaponTypeId, rarity, source, search, letter)).ToList(),
            WeaponTypes = (await _referenceRepo.GetAllWeaponTypesAsync()).ToList(),
            WeaponTypeId = weaponTypeId,
            Rarity = rarity,
            Source = source,
            Search = search,
            Letter = letter
        };

        return View(vm);
    }

    /// <summary>
    /// Fiche détail d'une arme.
    /// </summary>
    public async Task<IActionResult> Details(int id)
    {
        var weapon = await _weaponRepo.GetByIdAsync(id);

        if (weapon is null)
            return NotFound();

        return View(weapon);
    }
}