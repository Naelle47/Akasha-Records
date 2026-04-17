using Akasha_Records.Models;
using AkashaRecords.Data.Repositories.BuildRepo;
using AkashaRecords.Data.Repositories.CharacterRepo;
using AkashaRecords.Data.Repositories.WeaponRepo;
using AkashaRecords.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AkashaRecords.Controllers;

/// <summary>
/// Contrôleur de la page d'accueil.
/// </summary>
public class HomeController : Controller
{
    private readonly ICharacterRepository _characterRepo;
    private readonly IBuildRepository _buildRepo;
    private readonly IWeaponRepository _weaponRepo;

    public HomeController(
        ICharacterRepository characterRepo,
        IBuildRepository buildRepo,
        IWeaponRepository weaponRepo)
    {
        _characterRepo = characterRepo;
        _buildRepo = buildRepo;
        _weaponRepo = weaponRepo;
    }

    /// <summary>
    /// Page d'accueil — stats globales, dernières sorties et derniers builds.
    /// </summary>
    public async Task<IActionResult> Index()
    {
        var vm = new HomeVM
        {
            CharacterCount = await _characterRepo.GetCountAsync(),
            WeaponCount = await _weaponRepo.GetCountAsync(),
            BuildCount = await _buildRepo.GetCountAsync(),
            LatestCharacters = (await _characterRepo.GetLatestAsync(6)).ToList(),
            LatestBuilds = (await _buildRepo.GetLatestAsync(3)).ToList()
        };

        return View(vm);
    }

    public IActionResult Privacy() => View();

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() => View(new ErrorViewModel
    {
        RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
    });
}