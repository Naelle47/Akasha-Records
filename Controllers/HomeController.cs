using AkashaRecords.Data.Repositories.CharacterRepo;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AkashaRecords.Controllers;

/// <summary>
/// Contrôleur de la page d'accueil.
/// </summary>
public class HomeController : Controller
{
    private readonly ICharacterRepository _characterRepo;

    /// <summary>
    /// Initialise le contrôleur avec le repository des personnages via injection de dépendances.
    /// </summary>
    public HomeController(ICharacterRepository characterRepo)
    {
        _characterRepo = characterRepo;
    }

    /// <summary>
    /// Affiche la page d'accueil avec les 5 derniers personnages ajoutés.
    /// </summary>
    public async Task<IActionResult> Index()
    {
        var latest = await _characterRepo.GetLatestAsync(6);
        return View(latest.ToList());
    }

    /// <summary>
    /// Affiche la politique de confidentialité.
    /// À compléter si collecte de données personnelles (auth, emails...).
    /// </summary>
    public IActionResult Privacy()
    {
        return View();
    }

    /// <summary>
    /// Affiche la page d'erreur personnalisée.
    /// </summary>
    /// <remarks>
    /// TODO : Personnaliser la vue Error.cshtml avec le design du projet.
    /// Envisager d'afficher des messages différents selon le code HTTP (404, 500...).
    /// </remarks>
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View();
    }
}